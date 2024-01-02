from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from Configurations.Configurations import Configurations
from Utils.Json import Json
import warnings
import logging
class Session:
    def get_database_session():
        connection_string = f'mysql://{Configurations().database_username}:{Configurations().database_password}@{Configurations().database_ip}:{Configurations().database_porta}/{Configurations().database_name}'
        engine = create_engine(connection_string, echo=False)
        Session = sessionmaker(bind=engine)
        return Session()