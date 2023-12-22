from confluent_kafka import Producer, Message, KafkaException
from Utils.EnumMessageCode import MessageCode
from Utils.EnumMessageType import MessageType
from Utils.Json import Json
import pickle
from datetime import datetime
from DB.Repository.MessageSentRepo import MessageSentRepo
from DB.Model import MessageSent
from queue import Queue
class ProducerClass:
    offset_queue = Queue()
    def delivery_report(err, msg):
        if err is not None:
            print(f'Errore durante la produzione del messaggio: {err}')
        else:
            print(f'Produced message {msg.value().decode("utf-8")} on topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()}')       
            ProducerClass.offset_queue.put(msg)
            return msg
        
    def send_response(header, msg, topic,cod):
        producer_config = Json.leggi_configurazioni("configuration_producer")
        producer = Producer(producer_config)
        try:
            producer.produce(
                topic, 
                key='key',  
                value=str(msg),
                headers=header,
                callback=ProducerClass.delivery_report
            )
            producer.flush()
        except KafkaException as e:
            print(f'Errore durante la produzione del messaggio: {e}')
        msgSent = ProducerClass.offset_queue.get()
        ProducerClass.saveMessage(msgSent,header,topic,cod)
        return msgSent.offset()
        
    def send_message(header,msg,topic):
        producer_config = Json.leggi_configurazioni("configuration_producer")
        producer = Producer(producer_config)
        producer.produce(topic, key='key', value=str(msg),header=header, callback=ProducerClass.delivery_report)
        producer.flush()
        offset = ProducerClass.offset_queue.get()
        return offset
    
    def saveMessage(result,header,topic,cod):
        header_cleaned = dict(header)
        new=MessageSent()
        new.message=str(result.value().decode("utf-8"))
        new.offset=result.offset()
        new.timestamp=datetime.utcnow()
        new.tagMessage= header_cleaned.get('Tag')
        new.type=header_cleaned.get('Type')
        new.idOffsetResponse=int(header_cleaned.get('IdOffsetResponse'))
        new.creator=header_cleaned.get('Creator')
        new.topic=topic
        new.partition=result.partition()
        new.code=cod
        MessageSentRepo.add_message(new)
        

