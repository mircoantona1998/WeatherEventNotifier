import json
from confluent_kafka import Consumer, KafkaError, TopicPartition
from Configurations.Configurations import Configurations
from DB.Repository.HeartbeatSentRepo import HeartbeatSentRepo
from DB.Repository.MessageReceivedRepo import MessageReceivedRepo
from DB.Repository.ScheduleRequestRepo import ScheduleRequestRepo
from DB.Repository.RequestNotificationRepo import RequestNotificationRepo
from DB.Repository.ScheduleResponseRepo import ScheduleResponseRepo
from Handler.event_destinators import GestoreDestinatari
from Kafka.KafkaHeader import KafkaHeader
from Kafka.Producer import ProducerClass
from Utils.Heartbeat import Heartbeat
from Utils.EnumMessageCode import MessageCode
from Utils.EnumMessageType import MessageType
from Utils.Json import Json
from DB.Model import MessageReceived
from Handler.event_handlers import EventHandlers
from confluent_kafka.admin import AdminClient, NewTopic
from Utils.Logger import Logger
import inspect
from datetime import datetime, timedelta
class ConsumerClass:
    def run_request(self):
        consumer={}
        consumer["bootstrap.servers"]=Configurations().consumer_bootstrap_servers
        consumer["group.id"]=Configurations().consumer_group_id
        consumer["auto.offset.reset"]=Configurations().consumer_auto_offset_reset
        creator=Configurations().group_id
        consumer = Consumer(consumer)
        topic = Configurations().topic_to_scheduler
        admin_client_config = {'bootstrap.servers': Configurations().consumer_bootstrap_servers}
        admin_client = AdminClient(admin_client_config)
        new_topic = NewTopic(
            topic=topic,
            num_partitions=1,
            replication_factor=1  
        )
        topic_created = False
        while not topic_created:
            try:
                admin_client.create_topics([new_topic])
                topic_created = True
                print("Topic  creato con successo.")
                Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Topic  creato con successo. - {inspect.currentframe().f_globals['__file__']}")
            except Exception as e:
                if "AlreadyExistsError" in str(e):
                    topic_created = True
                    print("Il topic  esiste.")
                    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Il topic  esiste. - {inspect.currentframe().f_globals['__file__']}")
                else:
                    print(f"Errore durante la creazione del topic: {e}")
                    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Errore durante la creazione del topic: {e} - {inspect.currentframe().f_globals['__file__']}")
        partitions_to_subscribe= [int(part) for part in Configurations().partition]
        consumer.assign([TopicPartition(topic=topic, partition=part) for part in partitions_to_subscribe])
        try:
            while True:
                if (HeartbeatSentRepo.get_last_element() is None  
                    or (HeartbeatSentRepo.get_last_element() is not None
                        and (datetime.utcnow().replace(second=0, microsecond=0) - HeartbeatSentRepo.get_last_element().datetime)
                      >= timedelta(minutes=int(Configurations().heartbeatfrequency)))):
                    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - HEARTBEAT - {inspect.currentframe().f_globals['__file__']}")
                    result=Heartbeat.message()
                    if result==True:
                            HeartbeatSentRepo.add_heartbeat_sent()      
                elif  ScheduleRequestRepo.get_last_element()==None or ScheduleRequestRepo.get_last_element().date!=datetime.utcnow().date() :
                    #produrre richiesta per avere tutte le configurazioni con isactive=true e datetimeactivation < della data di oggi alle 23:59, e aggiungere a schedulazioni
                    headersRequest= KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Request.value ,Tag="GetConfigurationForToday", Creator=creator, Code = MessageCode.Ok.value)
                    ProducerClass.send_message(headersRequest.headers_list,json.dumps({'Data': ""}, indent=2),GestoreDestinatari().determina_destinatario("ConfiguratorService"))            
                    print(f'{str(headersRequest.to_string())}')
                    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersRequest.to_string())} - {inspect.currentframe().f_globals['__file__']}")
                    ScheduleRequestRepo.add_request_schedule()
                elif (
                    ScheduleResponseRepo.get_last_element() is not None
                    and ScheduleResponseRepo.get_last_element().date == datetime.utcnow().date()
                    and (
                        RequestNotificationRepo.get_last_element() is None
                        or (
                            (datetime.utcnow().replace(second=0, microsecond=0) - RequestNotificationRepo.get_last_element().datetime)
                            >= timedelta(minutes=5)
                        )
                    )
                ):
                     #invia a weather le schedulazioni corrent
                    handler = EventHandlers.tag_handlers.get("SchedulationCurrent", lambda: f"Tag SchedulationCurrent non gestito")
                    responses = handler()
                    for response in responses:
                        headersRequest= KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Response.value ,Tag="SchedulationCurrent", Creator=creator, Code = MessageCode.Ok.value)
                        ProducerClass.send_message(headersRequest.headers_list,json.dumps({'Data': response}, indent=2),GestoreDestinatari().determina_destinatario("WeatherService"))            
                        print(f'{str(headersRequest.to_string())}')
                        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersRequest.to_string())} - {inspect.currentframe().f_globals['__file__']}")
                    RequestNotificationRepo.add_request_notification()
                else:
                        msg = consumer.poll(1.0)
                        if msg is not None:
                        #if MessageReceivedRepo.get_latest_message() ==None or MessageReceivedRepo.get_latest_message().offset==None or (msg.offset()!=None and msg.offset() > MessageReceivedRepo.get_latest_message().offset):
                            print(f'Received message {str(msg.value().decode("utf-8"))} on topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()}')     
                            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Received message {str(msg.value().decode('utf-8'))} on topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()} - {inspect.currentframe().f_globals['__file__']}")
                            value=msg.value().decode("utf-8")
                            partition = msg.partition()
                            headers = msg.headers()
                            if headers!= None :
                                headers_dict = {key: value.decode('utf-8') if isinstance(value, bytes) else value for key, value in headers}
                                if len(headers_dict)>0:
                                    header= KafkaHeader(header=headers_dict)
                                    print(f'{str(header.to_string())}')
                                    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(header.to_string())} - {inspect.currentframe().f_globals['__file__']}")
                                    ConsumerClass.saveMessage(msg,header)
                                    consumer.commit(message=msg)
                                    if header.Type==MessageType.Request.value:
                                        try:
                                            handler = EventHandlers.tag_handlers.get(header.Tag, lambda: f"Tag {header.Tag} non gestito")
                                            response = handler(json.loads(value))
                                            if response!=None:
                                                headersResponse= KafkaHeader(IdOffsetResponse=msg.offset(),Type=MessageType.Response.value ,Tag=header.Tag, Creator=creator, Code = MessageCode.Ok.value)
                                                ProducerClass.send_message(headersResponse.headers_list,json.dumps({'Data': response}, indent=2),GestoreDestinatari().determina_destinatario(header.Creator))      
                                                print(f'{str(headersResponse.to_string())}')
                                                Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersResponse.to_string())} - {inspect.currentframe().f_globals['__file__']}")
                                        except Exception as ex:
                                            print(f'Error: {value}')
                                            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Error: {value} - {inspect.currentframe().f_globals['__file__']}")
                                            headersResponse= KafkaHeader(IdOffsetResponse= msg.offset(),Type = MessageType.Response.value,Tag=header.Tag, Creator = creator, Code = MessageCode.Error.value)
                                            print(f'{str(headersResponse.to_string())}')
                                            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersResponse.to_string())} - {inspect.currentframe().f_globals['__file__']}")
                                            ProducerClass.send_message(headersResponse.headers_list,{'Data': str(ex)},GestoreDestinatari().determina_destinatario(header.Creator))         
                                    elif header.Type==MessageType.Response.value:
                                            handler = EventHandlers.tag_handlers.get(header.Tag, lambda: f"Tag {header.Tag} non gestito")
                                            handler(json.loads(value))
                                else:
                                    ConsumerClass.saveMessageWithError(msg)   
                                    consumer.commit(message=msg)
                            else:
                                ConsumerClass.saveMessageWithError(msg)   
                                consumer.commit(message=msg)
                        if msg is None:
                            continue
                        if msg.error():
                            if msg.error().code() == KafkaError._PARTITION_EOF:
                                continue
                            else:
                                print(msg.error())
                                continue
        except Exception as ex:
            print(str(ex))
            pass
        finally:
            consumer.close()
            
    def saveMessage(msg,header):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - saveMessage - {inspect.currentframe().f_globals['__file__']}")
        new=MessageReceived()
        new.message=msg.value().decode("utf-8")
        new.offset=msg.offset()
        new.timestamp=datetime.utcnow()
        new.tagMessage=header.Tag
        new.creator=header.Creator
        new.type=header.Type
        new.idOffsetResponse=header.IdOffsetResponse
        new.topic=msg.topic()
        new.partition=msg.partition()
        new.code=MessageCode.Ok.value
        MessageReceivedRepo.add_message(new)
              
    def saveMessageWithError(msg):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - saveMessageWithError - {inspect.currentframe().f_globals['__file__']}")
        new=MessageReceived()
        new.message=msg.value().decode("utf-8")
        new.offset=msg.offset()
        new.timestamp=datetime.utcnow()
        new.topic=msg.topic()
        new.partition=msg.partition()
        new.code=MessageCode.Error.value
        MessageReceivedRepo.add_message(new)
        



