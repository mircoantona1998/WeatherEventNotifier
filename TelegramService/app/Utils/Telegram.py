import requests
from Utils.Logger import Logger
import inspect
from datetime import datetime
class Telegram:

    def send_message(self, chat_id, text,bot_token):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - send_message - {inspect.currentframe().f_globals['__file__']}")
        api_url = f"https://api.telegram.org/bot{bot_token}/sendMessage"
        data = {
            'chat_id': chat_id,
            'text': text
        }
        response = requests.post(api_url, json=data)
        return response.json()