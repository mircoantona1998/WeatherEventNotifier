from datetime import datetime

from DB.Repository.WeatherRepo import WeatherRepo
from Utils.OpenWeather import OpenWeatherAPI

class EventHandlers:  
     
    def handle_tag_GetMeasure(data):
        return WeatherRepo.get_measure(data)
    
    def handle_tag_AddMeasure(data):
        #dataResult=OpenWeatherAPI.get_forecast_by_coords(data[""],data[""],data[""])
        dataResult=OpenWeatherAPI.get_forecast_by_coords(45.4642,9.1900,"7922142ac1c5839b29f90140be5565ec")
        return WeatherRepo.add_measure(dataResult)
    
    tag_handlers = {    
       "GetMeasure": handle_tag_GetMeasure,
       "AddMeasure": handle_tag_AddMeasure,
    }
