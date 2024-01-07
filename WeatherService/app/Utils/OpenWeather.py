import requests
from Utils.Logger import Logger
import inspect
from datetime import datetime
class OpenWeatherAPI:

    def get_forecast_by_coords( lat, lon,api_key, num_hours=1):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_forecast_by_coords - {inspect.currentframe().f_globals['__file__']}")
        endpoint = '/forecast'
        params = {
            'lat': lat,
            'lon': lon,
            'appid': api_key,
            'units': 'metric',
            'cnt': num_hours
        }
        response = OpenWeatherAPI._make_request(endpoint, params)
        return response.json()

    def _make_request( endpoint, params):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - _make_request - {inspect.currentframe().f_globals['__file__']}")
        base_url='https://api.openweathermap.org/data/2.5'
        url = f'{base_url}{endpoint}'
        response = requests.get(url, params=params)
        response.raise_for_status()
        return response

