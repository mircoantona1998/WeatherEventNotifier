import requests
class Telegram:

    def send_message(self, chat_id, text,bot_token):
        api_url = f"https://api.telegram.org/bot{bot_token}/sendMessage"
        data = {
            'chat_id': chat_id,
            'text': text
        }
        response = requests.post(api_url, json=data)
        return response.json()