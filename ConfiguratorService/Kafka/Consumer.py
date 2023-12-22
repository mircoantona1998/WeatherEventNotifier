from datetime import datetime
from email.header import Header
import json
from confluent_kafka import Consumer, KafkaError
from sqlalchemy.sql import null
from DB.Repository.ConfigurationUserRepo import ConfigurationUserRepo
from DB.Repository.MessageReceivedRepo import MessageReceivedRepo
from Kafka.KafkaHeader import KafkaHeader
from Kafka.Producer import ProducerClass
from Utils.EnumMessageCode import MessageCode
from Utils.EnumMessageType import MessageType
from Utils.Json import Json
from sqlalchemy import create_engine
from DB.Model import MessageReceived
from Handler.event_handlers import EventHandlers

class ConsumerClass:
    
    def run_request(self):
        configurazione_consumer = Json.leggi_configurazioni("configuration_consumer")
        configurazione_producer = Json.leggi_configurazioni("configuration_producer")
        creator=Json.leggi_configurazioni("group.id")
        consumer = Consumer(configurazione_consumer)
        topic = Json.leggi_configurazioni("topic_to_configuration")
        consumer.subscribe([topic])
        try:
            while True:
                msg = consumer.poll(1.0)
                if msg is not None:
                    if msg.offset() is not None:                  
                        if MessageReceivedRepo.get_latest_message() ==None or msg.offset() > MessageReceivedRepo.get_latest_message().offset:
                            print(f'Received message {msg.value().decode("utf-8")} on topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()}')     
                            value=msg.value().decode("utf-8")
                            partition = msg.partition()
                            headers = msg.headers()
                            if headers!= None :
                                headers_dict = {key: value.decode('utf-8') if isinstance(value, bytes) else value for key, value in headers}
                                if len(headers_dict)>0:
                                    header= KafkaHeader(header=headers_dict)
                                    ConsumerClass.saveMessage(msg,header)
                                    try:
                                        handler = EventHandlers.tag_handlers.get(header.Tag, lambda: f"Tag {header.Tag} non gestito")
                                        valueDict = json.loads(value)
                                        response = handler(valueDict)
                                        json_data = {'Data': response}
                                        json_response = json.dumps(json_data, indent=2)
                                        headersResponse= KafkaHeader(IdOffsetResponse=msg.offset(),Type=MessageType.Response.value ,Tag=header.Tag,Creator=creator,Code=True)
                                        ProducerClass.send_response(headersResponse.headers_list,json_response,Json.leggi_configurazioni("topic_to_userdata"),MessageCode.Ok.value)                                    
                                    except Exception as ex:
                                        print(f'Error: {value}')
                                        headersResponse= KafkaHeader(IdOffsetResponse= msg.offset(),Type = MessageType.Response.value,Tag=header.Tag, Creator = creator, Code = MessageCode.Error.value)
                                        json_data = {'Data': str(ex)}
                                        ProducerClass.send_response(headersResponse.headers_list,json_data,Json.leggi_configurazioni("topic_to_userdata"),MessageCode.Error.value)         
                                        pass
                                else:
                                    ConsumerClass.saveMessageWithError(msg)   
                                    headersResponse= KafkaHeader(IdOffsetResponse= msg.offset(),Type = MessageType.Response.value, Creator = creator, Code = MessageCode.Error.value)
                                    json_data = {'Data': "Without header"}
                                    ProducerClass.send_response(headersResponse.headers_list,json_data,Json.leggi_configurazioni("topic_to_userdata"),MessageCode.Error.value)        
                            else:
                                ConsumerClass.saveMessageWithError(msg)   
                                headersResponse= KafkaHeader(IdOffsetResponse= msg.offset(),Type = MessageType.Response.value, Creator = creator, Code = MessageCode.Error.value)
                                json_data = {'Data': "Without header"}
                                ProducerClass.send_response(headersResponse.headers_list,json_data,Json.leggi_configurazioni("topic_to_userdata"),MessageCode.Error.value)         
                    if msg is None:
                        continue
                    if msg.error():
                        if msg.error().code() == KafkaError._PARTITION_EOF:
                            continue
                        else:
                            print(msg.error())
                            break
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
        



