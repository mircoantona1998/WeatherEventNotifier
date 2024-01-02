import requests

class OpenWeatherAPI:

    def get_forecast_by_coords( lat, lon,api_key, num_hours=1):
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
        base_url='https://api.openweathermap.org/data/2.5'
        url = f'{base_url}{endpoint}'
        response = requests.get(url, params=params)
        response.raise_for_status()
        return response

