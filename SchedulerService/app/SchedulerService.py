import time
from datetime import datetime
import pyodbc
import os
import json
from common.db import Base
from common.db.session_scope import session_scope
import pytz
import requests
from sqlalchemy import text

class SchedulerService:
   
    italian_timezone = pytz.timezone('Europe/Rome')
    
    @staticmethod       
    def to_work(now_italian):
        current_directory = os.path.dirname(__file__)  
        json_file_path = os.path.join(current_directory, "customization", "config_db_sqlserver.json")
        with open(json_file_path, "r") as json_file:
            json_data = json.load(json_file)
        driver = json_data["DRIVER"]
        server = json_data["SERVER"]
        database = json_data["DATABASE"]
        uid = json_data["UID"]
        pwd = json_data["PWD"]
        connection_string = f"DRIVER={driver};SERVER={server};DATABASE={database};UID={uid};PWD={pwd}"
        connection = pyodbc.connect(connection_string)       
        try:
            query = "SELECT * FROM Jobs where Job='Alarm'"
            cursor = connection.cursor()         
            cursor.execute(query)
            Jobs = cursor.fetchall() 
            for job in Jobs:
                if(job.IsActive):
                    now = datetime.utcnow()     
                    italian_timezone = pytz.timezone('Europe/Rome')
                    current_datetime = now.astimezone(italian_timezone)
                    data_odierna = current_datetime.date()
                    hour=0
                    minute=0
                    if job.HourToStart!=None:
                        hour= job.HourToStart
                    if job.MinuteToStart!=None:
                        minute= job.MinuteToStart
                    data_attivazione = datetime.combine(data_odierna,time(hour, minute, 0))  
                    if(job.LastTimestampStart):           
                        if   current_datetime.date()>job.LastTimestampStart.date() and current_datetime.timestamp()>data_attivazione.timestamp():
                            return True;
                        else: return False;
                    else:
                        if   current_datetime.timestamp()>=data_attivazione.timestamp():
                            return True;
                        else: return False;
                else:
                    return False;
        except Exception as e:
            print(str(e))
            return False;
        finally:
            cursor.close()
            connection.close() 
        return False;

    @staticmethod       
    def start_job():
        try:    
            now = datetime.utcnow()     
            italian_timezone = pytz.timezone('Europe/Rome')
            now_italian_timezone = now.astimezone(italian_timezone)
            now_italian_timezone = now_italian_timezone.strftime('%Y-%m-%d %H:%M:%S')
            current_directory = os.path.dirname(__file__)  
            json_file_path = os.path.join(current_directory, "customization", "config_db_sqlserver.json")
            with open(json_file_path, "r") as json_file:
                json_data = json.load(json_file)
            driver = json_data["DRIVER"]
            server = json_data["SERVER"]
            database = json_data["DATABASE"]
            uid = json_data["UID"]
            pwd = json_data["PWD"]
            connection_string = f"DRIVER={driver};SERVER={server};DATABASE={database};UID={uid};PWD={pwd}"
            connection = pyodbc.connect(connection_string)          
            query = f"UPDATE [dbo].[Jobs] SET [LastTimestampStart] = '{now_italian_timezone}' WHERE Job='Scheduler'"
            cursor = connection.cursor()         
            cursor.execute(query)
            connection.commit()
        except Exception as e:
            print(str(e))
        finally:
            cursor.close()
            connection.close() 
        
    @staticmethod       
    def refresh_status(bool):
        try:    
            now = datetime.utcnow()     
            italian_timezone = pytz.timezone('Europe/Rome')
            now_italian_timezone = now.astimezone(italian_timezone)
            now_italian_timezone = now_italian_timezone.strftime('%Y-%m-%d %H:%M:%S')
            current_directory = os.path.dirname(__file__)  
            json_file_path = os.path.join(current_directory, "customization", "config_db_sqlserver.json")
            with open(json_file_path, "r") as json_file:
                json_data = json.load(json_file)
            driver = json_data["DRIVER"]
            server = json_data["SERVER"]
            database = json_data["DATABASE"]
            uid = json_data["UID"]
            pwd = json_data["PWD"]
            connection_string = f"DRIVER={driver};SERVER={server};DATABASE={database};UID={uid};PWD={pwd}"
            connection = pyodbc.connect(connection_string)          
            query = f"UPDATE [dbo].[Jobs] SET [LastTimestampEnd] = '{now_italian_timezone}',[Errors] = {1} WHERE Job='Scheduler'"
            cursor = connection.cursor()         
            cursor.execute(query)
            connection.commit()
        except Exception as e:
            print(str(e))
        finally:
            cursor.close()
            connection.close() 
    
    @staticmethod       
    def start_engine_weather_database():
        try:   
             now_italian = datetime.now( SchedulerService.italian_timezone)
             now_italian_timezone = now_italian.strftime('%Y-%m-%d %H:%M:%S') 
             with session_scope(__name__) as session:
                    try:
                        # Example of calling a stored procedure in MySQL using SQLAlchemy
                        result = session.execute(text("CALL Weather.engine_timer_tick(:param)"), {'param': now_italian_timezone})
                    except Exception as e:
                        print(f"An error occurred: {e}")
        except Exception as e:
            print(str(e))
            
    @staticmethod       
    def start_engine_userdata_database():
        try:    
            now_italian = datetime.now( SchedulerService.italian_timezone)
            now_italian_timezone = now_italian.strftime('%Y-%m-%d %H:%M:%S') 
            current_directory = os.path.dirname(__file__)  
            json_file_path = os.path.join(current_directory, "customization", "config_db_sqlserver.json")
            with open(json_file_path, "r") as json_file:
                json_data = json.load(json_file)
            driver = json_data["DRIVER"]
            server = json_data["SERVER"]
            database = json_data["DATABASE"]
            uid = json_data["UID"]
            pwd = json_data["PWD"]
            connection_string = f"DRIVER={driver};SERVER={server};DATABASE={database};UID={uid};PWD={pwd}"
            connection = pyodbc.connect(connection_string)
            cursor = connection.cursor()
            cursor.execute(f"EXEC EngineTimerTick @dateCreate=?", now_italian_timezone)
            connection.commit()
            print("Stored procedure eseguita con successo.")                     
        except Exception as e:
            print(f"An error occurred: {e}")
        finally:
            cursor.close()
            connection.close()
            
    @staticmethod
    def run():
        try:
            print('start')
            while True:    
                SchedulerService.start_job()
                SchedulerService.start_engine_weather_database()
                SchedulerService.start_engine_userdata_database()
                SchedulerService.refresh_status(False)
                time.sleep(5)
        except Exception as e:
            print("Impossibile completare "+ str(e))
        
if __name__ == '__main__':
    SchedulerService.run()
