from confluent_kafka import Producer, KafkaException
import pickle
from datetime import datetime
from DB.Repository.MessageSentRepo import MessageSentRepo
from DB.Model import MessageSent
from queue import Queue
from Configurations.Configurations import Configurations

class ProducerClass:
    offset_queue = Queue()
    def delivery_report(err, msg):
        if err is not None:
            print(f'Errore durante la produzione del messaggio: {err}')
        else:
            print(f'Produced message {str(msg.value().decode("utf-8"))} on topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()}')       
            ProducerClass.offset_queue.put(msg)
            return msg
        
    def send_message(header, msg, topic):
        producer={}
        producer["bootstrap.servers"]=Configurations().producer_bootstrap_servers
        producer["group.id"]=Configurations().producer_client_id
        producer = Producer(producer)
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
        ProducerClass.saveMessage(msgSent,header,topic)
        return msgSent.offset()
    
    def saveMessage(result,header,topic):
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
        new.code=header_cleaned.get('Code')
        MessageSentRepo.add_message(new)
        

