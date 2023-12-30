from DB.Session import Session
from DB.Model import MailUsers

class MailUsersRepo:
    
    def get_all():
        with Session.get_database_session() as session:
            return session.query(MailUsers).all()
   
    def add_message(data):
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
  
    def get_latest_message():
        with Session.get_database_session() as session:
            latest_message = session.query(MailUsers).order_by(MailUsers.id.desc()).first()
            return latest_message

