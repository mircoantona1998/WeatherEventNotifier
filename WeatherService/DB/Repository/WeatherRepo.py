from datetime import datetime, timedelta
from DB.Session import Session
from DB.Model import Weather
from Utils.OpenWeather import OpenWeatherAPI

class WeatherRepo:
        
    def get_measure(id_user=None):
        if id_user is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Weather)
            query = query.filter_by(IdUser=id_user)
            return query.first() 
        
    def add_measure(new_element_data):
        with Session.get_database_session() as session:
            current_datetime = datetime.now().replace(minute=0, second=0, microsecond=0)
            #new_element_data['DateTimeToSchedule'] = current_datetime + timedelta(minutes=minutes_freq)       
            while current_datetime <= datetime.now().replace(hour=23, minute=59, second=59, microsecond=0):  # Continua fino a mezzanotte
                new_element = Weather(**new_element_data)
                session.add(new_element)
                session.commit()
               # current_datetime += timedelta(minutes=minutes_freq)
            return 
                      
   