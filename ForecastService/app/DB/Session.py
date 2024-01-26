from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from sqlalchemy.exc import OperationalError
from Configurations.Configurations import Configurations
from Utils.Logger import Logger
import time
from datetime import datetime
import inspect
import pyodbc

class Session:
    @staticmethod
    def get_database_session():
        connection_string = (
            f'mssql+pyodbc://{Configurations().database_username}:{Configurations().database_password}@'
            f'{Configurations().database_ip}:{Configurations().database_porta}/'
            f'{Configurations().database_name}?driver=ODBC+Driver+17+for+SQL+Server'
        )
        engine = create_engine(connection_string, echo=False)
        Session = sessionmaker(bind=engine)
        return Session()

    @staticmethod
    def wait_for_sql_server(timeout=60):
        start_time = time.time()
        while True:
            try:
                connection_string = (
                    f'DRIVER=ODBC Driver 17 for SQL Server;'
                    f'SERVER={Configurations().database_ip},{Configurations().database_porta};'
                    f'DATABASE={Configurations().database_name};'
                    f'UID={Configurations().database_username};'
                    f'PWD={Configurations().database_password}'
                )
                try:
                    with pyodbc.connect(connection_string, autocommit=True) as connection:
                        print("Connection successful!")
                        return True
                except pyodbc.Error as ex:
                    print(f"Error connecting to SQL Server: {ex}")
            except OperationalError as e:
                print(f"Database not available yet. Retrying in 5 seconds... Error: {str(e)}")
                Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Database not available yet. Retrying in 5 seconds... Error: {str(e)} - {inspect.currentframe().f_globals['__file__']}")
                time.sleep(5)
