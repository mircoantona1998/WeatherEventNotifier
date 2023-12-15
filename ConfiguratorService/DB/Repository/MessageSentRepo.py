from DB.Session import Session
from DB.Model import MessageSent

class MessageSentRepo:
    
    def get_all():
        with Session.get_database_session() as session:
            return session.query(MessageSent).all()
 
    def add_message(data):
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
    
    def get_latest_message():
        with Session.get_database_session() as session:
            latest_message = session.query(MessageSent).order_by(MessageSent.id.desc()).first()
            return latest_message
