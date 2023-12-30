import requests
class Telegram:
    def __init__(self):
        self.bot_token = "6783949622:AAHVzLeQ_WV_vx5YZAk8jJeNPe6fl64U2Zg"

    def send_message(self, chat_id, text):
        api_url = f"https://api.telegram.org/bot{self.bot_token}/sendMessage"
        data = {
            'chat_id': chat_id,
            'text': text
        }
        response = requests.post(api_url, json=data)
        return response.json()