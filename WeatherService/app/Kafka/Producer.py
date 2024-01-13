from confluent_kafka import Producer, KafkaException
import pickle
from datetime import datetime
from DB.Repository.MessageSentRepo import MessageSentRepo
from DB.Model import MessageSent
from queue import Queue
from Configurations.Configurations import Configurations
from Utils.Logger import Logger
import inspect
from datetime import datetime
class ProducerClass:
    offset_queue = Queue()
    def delivery_report(err, msg):
        if err is not None:
            print(f'Errore durante la produzione del messaggio: {err}')
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Errore durante la produzione del messaggio: {err} - {inspect.currentframe().f_globals['__file__']}")
        else:
            print(f'Produced message {str(msg.value().decode("utf-8"))} on topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()}')       
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Produced message {str(msg.value().decode('utf-8'))} on topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()}- {inspect.currentframe().f_globals['__file__']}")
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
                callback=ProducerClass.delivery_report,
                partition=int(Configurations().partition)
            )
            producer.flush()
        except KafkaException as e:
            print(f'Errore durante la produzione del messaggio: {e}')
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Errore durante la produzione del messaggio: {e} - {inspect.currentframe().f_globals['__file__']}")
        msgSent = ProducerClass.offset_queue.get()
        ProducerClass.saveMessage(msgSent,header,topic)
        return msgSent.offset()
    
    def saveMessage(result,header,topic):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - saveMessage - {inspect.currentframe().f_globals['__file__']}")
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
        

