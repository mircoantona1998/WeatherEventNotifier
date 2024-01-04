from datetime import datetime
import json
from confluent_kafka import Consumer, KafkaError
from Configurations.Configurations import Configurations
from DB.Repository.MessageReceivedRepo import MessageReceivedRepo
from Handler.event_destinators import GestoreDestinatari
from Kafka.KafkaHeader import KafkaHeader
from Kafka.Producer import ProducerClass
from Utils.EnumMessageCode import MessageCode
from Utils.EnumMessageType import MessageType
from DB.Model import MessageReceived
from Handler.event_handlers import EventHandlers
from confluent_kafka.admin import AdminClient, NewTopic

class ConsumerClass:
    def run_request(self):
        consumer={}
        consumer["bootstrap.servers"]=Configurations().consumer_bootstrap_servers
        consumer["group.id"]=Configurations().consumer_group_id
        consumer["auto.offset.reset"]=Configurations().consumer_auto_offset_reset
        creator=Configurations().group_id
        consumer = Consumer(consumer)
        topic = Configurations().topic_to_weather
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
            except Exception as e:
                if "AlreadyExistsError" in str(e):
                    topic_created = True
                    print("Il topic  esiste.")
                else:
                    print(f"Errore durante la creazione del topic: {e}")


        consumer.subscribe([topic])
        try:
            while True:
                msg = consumer.poll(1.0)
                if msg is not None:
                    if MessageReceivedRepo.get_latest_message() ==None or MessageReceivedRepo.get_latest_message().offset==None or (msg.offset()!=None and msg.offset() > MessageReceivedRepo.get_latest_message().offset):
                        print(f'Received message {str(msg.value().decode("utf-8"))} on topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()}')     
                        value=msg.value().decode("utf-8")
                        partition = msg.partition()
                        headers = msg.headers()
                        if headers!= None :
                            headers_dict = {key: value.decode('utf-8') if isinstance(value, bytes) else value for key, value in headers}
                            if len(headers_dict)>0:
                                header= KafkaHeader(header=headers_dict)
                                ConsumerClass.saveMessage(msg,header)
                                if header.Type==MessageType.Request.value:
                                    try:
                                        handler = EventHandlers.tag_handlers.get(header.Tag, lambda: f"Tag {header.Tag} non gestito")
                                        response = handler(json.loads(value))
                                        if response!=None:
                                            headersResponse= KafkaHeader(IdOffsetResponse=msg.offset(),Type=MessageType.Response.value ,Tag="GenerateNotification", Creator=creator, Code = MessageCode.Ok.value)
                                            ProducerClass.send_message(headersResponse.headers_list,json.dumps({'Data': response}, indent=2),GestoreDestinatari().determina_destinatario(header.Creator))                                    
                                    except Exception as ex:
                                        print(f'Error: {value}')
                                        headersResponse= KafkaHeader(IdOffsetResponse= msg.offset(),Type = MessageType.Response.value,Tag=header.Tag, Creator = creator, Code = MessageCode.Error.value)
                                        ProducerClass.send_message(headersResponse.headers_list,{'Data': str(ex)},GestoreDestinatari().determina_destinatario(header.Creator))         
                                else:
                                    continue
                            else:
                                ConsumerClass.saveMessageWithError(msg)   
                        else:
                            ConsumerClass.saveMessageWithError(msg)   
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
        new=MessageReceived()
        new.message=msg.value().decode("utf-8")
        new.offset=msg.offset()
        new.timestamp=datetime.utcnow()
        new.topic=msg.topic()
        new.partition=msg.partition()
        new.code=MessageCode.Error.value
        MessageReceivedRepo.add_message(new)
        



