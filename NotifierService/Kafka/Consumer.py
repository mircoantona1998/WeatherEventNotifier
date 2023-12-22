from datetime import datetime
import json
from confluent_kafka import Consumer, KafkaError
from sqlalchemy.sql import null
from DB.Repository.MessageReceivedRepo import MessageReceivedRepo
from Kafka.KafkaMessage import KafkaMessage
from Kafka.Producer import ProducerClass
from Utils.EnumMessageType import MessageType
from Utils.Json import Json
from sqlalchemy import create_engine
from DB.Model import MessageReceived
from Handler.event_handlers import EventHandlers

class ConsumerClass:
    
    def run_request(self):
        configurazioni = Json.leggi_configurazioni("notifier_consumer")
        consumer = Consumer(configurazioni)
        topic = Json.leggi_configurazioni("topic_to_notifier")
        consumer.subscribe([topic])
        try:
            while True:
                msg = consumer.poll(1.0)
                if msg is not None:
                    if msg.offset() is not None:                  
                        if MessageReceivedRepo.get_latest_message() ==None or msg.offset() > MessageReceivedRepo.get_latest_message().offset:
                            try:
                                value=msg.value().decode("utf-8")
                                print(f'Received message: {value}')                             
                                message_received = Json.convert_json_to_object(value,KafkaMessage)
                                ConsumerClass.saveMessage(msg,message_received,topic)
                                tag = message_received.Tag                              
                                handler = EventHandlers.tag_handlers.get(tag, lambda: f"Tag {tag} non gestito")
                                response = handler( msg.offset(),message_received.Data,tag)
                                json_response = json.dumps(response)
                                offset=ProducerClass.send_response(json_response)     
                                ProducerClass.saveMessage(json_response,message_received,msg.offset(),offset,topic)
                            except Exception as ex:
                                print(f'Error: {value}')                              
                                kf=KafkaMessage(msg.offset(),MessageType.Response.value,tag,str(ex),False)
                                json_data = json.dumps(kf.__dict__, indent=2)
                                offset=ProducerClass.send_response(json_data)
                                ProducerClass.saveMessage(json_data,message_received,msg.offset(),offset,topic)
                                pass
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
            
                
    def saveMessage(msg,message_received,topic):
        new=MessageReceived()
        new.message=msg.value().decode("utf-8")
        new.offset=msg.offset()
        new.timestamp=datetime.utcnow()
        new.tagMessage=message_received.Tag
        new.type=message_received.Type
        if(new.type ==  MessageType.Request):
            new.idOffsetResponse=message_received.IdOffsetResponse
        new.topic=topic
        MessageReceivedRepo.add_message(new)
        



