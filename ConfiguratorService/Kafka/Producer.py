from confluent_kafka import Producer
from Kafka.KafkaMessage import KafkaMessage
from Utils.EnumMessageType import MessageType
from Utils.Json import Json
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
            print(f'Produced message {msg.value().decode("utf-8")} consegnato al topic {msg.topic()} [{msg.partition()}] @ offset {msg.offset()}')       
            ProducerClass.offset_queue.put(msg.offset())
            return msg.offset()
    def send_response(msg):
        producer_config = Json.leggi_configurazioni("configuration_producer")
        producer = Producer(producer_config)
        topic = Json.leggi_configurazioni("topic_to_userdata")
        producer.produce(topic, key='key', value=str(msg), callback=ProducerClass.delivery_report)
        producer.flush()
        offset = ProducerClass.offset_queue.get()
        return offset
        
    def saveMessage(msg,message_received,offsetResponse,offset,topic):
        new=MessageSent()
        new.message=str(msg)
        new.offset=offset
        new.timestamp=datetime.utcnow()
        new.tagMessage=message_received.Tag
        new.type=MessageType.Response.value
        if(new.type ==  MessageType.Response.value):
            new.idOffsetResponse=offsetResponse
        new.topic=topic
        MessageSentRepo.add_message(new)