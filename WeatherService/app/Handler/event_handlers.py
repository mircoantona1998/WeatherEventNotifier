from DB.Repository.ApiKeyRepo import ApiKeyRepo
from Utils.OpenWeather import OpenWeatherAPI
from Utils.Logger import Logger
import inspect
from datetime import datetime
class EventHandlers:  
    
    def handle_tag_SchedulationCurrent(dat):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_SchedulationCurrent - {inspect.currentframe().f_globals['__file__']}")
        key=ApiKeyRepo.get_key()
        data=dat["Data"]
        response=OpenWeatherAPI.get_forecast_by_coords(data["Latitude"],data["Longitude"],key.apiKey)
        if response["cod"]=="200":
            if data["ParentMetric"]!=None:
                weather=response["list"][0][data["ParentMetric"]][data["FieldMetric"]]
                if data["Symbol"]==">":
                    if float(weather)>float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]=="<" :
                    if float(weather)<float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]=="<=" :
                    if float(weather)<=float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]==">" :
                    if float(weather)>float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]==">=" :
                    if float(weather)>=float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]=="==" :
                    if float(weather)==float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
            else:
                weather=response["list"][0][data["FieldMetric"]]
                if data["Symbol"]==">" :
                    if float(weather)>float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]==">=" :
                    if float(weather)>=float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]=="<" :
                    if float(weather)<float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]=="<=" :
                    if float(weather)<=float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
                elif data["Symbol"]=="==" :
                    if float(weather)==float(data["Value"]):
                        dat["Data"]["Notify"]=True
                        dat["Data"]["ValueWeather"]=float(weather)
                        return dat["Data"]
                    else:
                        return None
        return None
    tag_handlers = {    
       "SchedulationCurrent": handle_tag_SchedulationCurrent,
    }
