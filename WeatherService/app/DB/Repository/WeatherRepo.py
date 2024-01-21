from Configurations.Configurations import Configurations
from DB.Session import Session
from DB.Model import Weather
from Utils.Logger import Logger
import inspect
from datetime import datetime
from sqlalchemy.sql import func
from sqlalchemy import select
from geopy.distance import great_circle
class WeatherRepo:
    
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            return session.query(Weather).all()
   
    def add_weather(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_weather - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_weather =data
            session.add(new_weather)
            session.commit()
  
    def get_weather_db(input_latitude, input_longitude, input_datetime):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_latest_weather - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            results = session.query(Weather).filter_by(datetime=input_datetime)
            for row in results:
                if great_circle((row.latitude, row.longitude), (input_latitude, input_longitude)).kilometers <= float(Configurations().distance_reuse_weather_km):
                    return row
        return None 

            