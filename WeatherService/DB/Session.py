from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from Utils.Json import Json
import warnings
import logging
class Session:
    def get_database_session():
        config = Json.leggi_configurazioni("database")
        connection_string = 'mysql://{username}:{password}@{ip}:{porta}/{database}'.format(**config)
        engine = create_engine(connection_string, echo=False)
        Session = sessionmaker(bind=engine)
        return Session()