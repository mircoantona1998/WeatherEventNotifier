import requests

class OpenWeatherAPI:
    def __init__(self, api_key, base_url='https://api.openweathermap.org/data/2.5'):
        self.api_key = api_key
        self.base_url = base_url

    # def get_current_weather_by_coords(self, lat, lon):
    #     endpoint = '/weather'
    #     params = {
    #         'lat': lat,
    #         'lon': lon,
    #         'appid': self.api_key,
    #         'units': 'metric'
    #     }
    #     response = self._make_request(endpoint, params)
    #     return response.json()

    def get_forecast_by_coords(self, lat, lon,api_key, num_hours=1):
        endpoint = '/forecast'
        params = {
            'lat': lat,
            'lon': lon,
            'appid': api_key,
            'units': 'metric',
            'cnt': num_hours
        }
        response = self._make_request(endpoint, params)
        return response.json()

    def _make_request(self, endpoint, params):
        url = f'{self.base_url}{endpoint}'
        response = requests.get(url, params=params)
        response.raise_for_status()
        return response

