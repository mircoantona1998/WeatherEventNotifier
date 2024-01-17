import requests
from Configurations.Configurations import Configurations
from Utils.Logger import Logger
from datetime import datetime
import inspect
class Heartbeat:
    def message(self):
        url = f"http://{Configurations().slamanager}:8081/Auth/Login"
        data = {
            "Servicename": Configurations().group_id +'_part'+ Configurations().partition,
            "password": Configurations().group_id +'_part'+ Configurations().partition,
        }
        response = requests.post(url, json=data)
        if response.status_code == 200:
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Heartbeat inviato con successo. - {inspect.currentframe().f_globals['__file__']}")
        else:
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Heartbeat NON inviato con successo. {response.status_code} - {inspect.currentframe().f_globals['__file__']}")
            print(response.text)  


