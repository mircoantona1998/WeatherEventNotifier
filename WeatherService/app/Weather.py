from datetime import datetime,time
import time
import logging
import requests
import os
import json
from common.db import Base
from common.db.session_scope import session_scope
import pytz
import pyodbc
from sqlalchemy import update

class Weather:
    #L'api_key è associato ad un account di prova gratuito. Sono possibili 50 chiamate al giorno.
    #La documentaione delle API di accuweather si trova a questo indirizzo: https://developer.accuweather.com/apis
    #Per ottenere la location_key (parametro obbligatorio per le API di weather), si può, conoscendo le coordinate
    #latitudine e longitudine utilizzare la seguente API:
    #https://developer.accuweather.com/accuweather-locations-api/apis/get/locations/v1/cities/geoposition/search
    #nella risposta il campo Key è la location_key.
    # icons: https://developer.accuweather.com/weather-icons
    # Al momento siamo limitati a 12 ore di hourly weather e 5 giorni of daily weather
    api_daily_1day = "http://dataservice.accuweather.com/weathers/v1/daily/1day/{location_key}?apikey={api_key}&details=true&metric=true"
    api_daily_5day = "http://dataservice.accuweather.com/weathers/v1/daily/5day/{location_key}?apikey={api_key}&details=true&metric=true"
    api_hourly_1h = "http://dataservice.accuweather.com/weathers/v1/hourly/1hour/{location_key}?apikey={api_key}&details=true&metric=true"
    api_hourly_12h = "http://dataservice.accuweather.com/weathers/v1/hourly/12hour/{location_key}?apikey={api_key}&details=true&metric=true"
    logging.basicConfig(level=logging.DEBUG)
    @staticmethod
    def append_entries_one1(payload, weather_hourly_list, first_key):
        try:
                weather_hourly_list.append(
                    Base.classes.weather_hourly_data(Field=first_key, Value=payload[first_key]["Value"],
                                                ValueUnit=payload[first_key]["Unit"], ValueType=(str(type(payload[first_key]['Value'])))[8:-2]))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))

    @staticmethod
    def append_entries_two1(payload, weather_hourly_list, first_key, second_key):
        try:
            weather_hourly_list.append(
                Base.classes.weather_hourly_data(Field=first_key + second_key, Value=payload[first_key][second_key]['Value'],
                                            ValueType=(str(type(payload[first_key][second_key]['Value'])))[8:-2],
                                            ValueUnit=payload[first_key][second_key]['Unit']))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))

    @staticmethod
    def append_entries_three1(payload, weather_hourly_list, first_key):
        try:
            weather_hourly_list.append(
                Base.classes.weather_hourly_data(Field=first_key, Value=payload[first_key],
                                            ValueType="int"))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))

    @staticmethod
    def append_entries_one(payload, weather_daily_list, first_key, second_key):
        try:
            if second_key == 'Phase':
                weather_daily_list.append(
                    Base.classes.weather_daily(Field=first_key + second_key, Value=payload[first_key][second_key],
                                                ValueType=(str(type(payload[first_key][second_key])))[8:-2]))
            else:
                weather_daily_list.append(
                    Base.classes.weather_daily(Field=first_key + second_key, Value=payload[first_key][second_key],
                                                ValueType='datetime'))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))
        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key + second_key, keys=payload.keys()))

    @staticmethod
    def append_entries_two(payload, weather_daily_list, first_key, second_key):
        try:
            weather_daily_list.append(
                Base.classes.weather_daily(Field=first_key + second_key, Value=payload[first_key][second_key]['Value'],
                                            ValueType=(str(type(payload[first_key][second_key]['Value'])))[8:-2],
                                            ValueUnit=payload[first_key][second_key]['Unit']))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))
        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key + second_key, keys=payload.keys()))

    @staticmethod
    def append_entries_three(payload, weather_daily_list, first_key, second_key):
        try:
            weather_daily_list.append(
                Base.classes.weather_daily(Field=first_key + second_key,
                                            Value=payload[first_key][second_key]['Value'],
                                            ValueType=(str(type(payload[first_key][second_key]['Value'])))[8:-2],
                                            ValueUnit=payload[first_key][second_key]['Unit'],
                                            ExtraValue=payload[first_key][second_key]['Phrase']))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))
        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key + second_key, keys=payload.keys()))

    @staticmethod
    def append_entries_four(payload, weather_daily_list, first_key):
        try:
            weather_daily_list.append(
                Base.classes.weather_daily(Field=first_key, Value=payload[first_key],
                                            ValueType=(str(type(payload[first_key])))[8:-2]))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))
        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key, keys=payload.keys()))

    @staticmethod
    def append_entries_day_night_one(payload, weather_daily_detail_list, first_key, second_key):
        try:
            weather_daily_detail_list.append(
                Base.classes.weather_daily_detail(Field=second_key,
                                                   Value=payload[first_key][second_key]['Value'],
                                                   ValueType=(str(type(payload[first_key][second_key]['Value'])))[8:-2],
                                                   ValueUnit=payload[first_key][second_key]['Unit']))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))
        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key + second_key, keys=payload.keys()))

    @staticmethod
    def append_entries_day_night_two(payload, weather_daily_detail_list, first_key, second_key):
        try:
            weather_daily_detail_list.append(
                Base.classes.weather_daily_detail(Field=second_key, Value=payload[first_key][second_key],
                                                   ValueType=(str(type(payload[first_key][second_key])))[8:-2]))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))
        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key + second_key, keys=payload.keys()))

    @staticmethod
    def append_entries_day_night_three(payload, weather_daily_detail_list, first_key, second_key, third_key):
        try:
            weather_daily_detail_list.append(
                Base.classes.weather_daily_detail(Field=second_key + third_key,
                                                   Value=payload[first_key][second_key][third_key]['Value'],
                                                   ValueType=(str(type(
                                                       payload[first_key][second_key][third_key]['Value'])))[8:-2],
                                                   ValueUnit=payload[first_key][second_key][third_key]['Unit']))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))

        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key + second_key, keys=payload.keys()))

    @staticmethod
    def append_entries_day_night_four(payload, weather_daily_detail_list, first_key, second_key, third_key):
        try:
            weather_daily_detail_list.append(
                Base.classes.weather_daily_detail(Field=second_key + third_key,
                                                   Value=payload[first_key][second_key][third_key]['Degrees'],
                                                   ValueType=(str(type(
                                                       payload[first_key][second_key][third_key]['Degrees'])))[8:-2],
                                                   DetailExtraValue=payload[first_key][second_key][third_key][
                                                       'Localized']))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))
        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key + second_key, keys=payload.keys()))

    @staticmethod
    def append_entries_pollen(payload, weather_daily_detail_list, first_key):
        try:
            for el in payload[first_key]:
                if el['Name'] == 'AirQuality':
                    weather_daily_detail_list.append(
                        Base.classes.weather_daily_detail(Field=el['Name'], Value=el['Value'],
                                                           ValueType=(str(type(payload[first_key])))[8:-2],
                                                           DetailExtraValue=el['Category'] + ' (Type : ' + el[
                                                               'Type'] + ')'))
                else:
                    weather_daily_detail_list.append(
                        Base.classes.weather_daily_detail(Field=el['Name'], Value=el['Value'],
                                                           ValueType=(str(type(el['Value'])))[8:-2],
                                                           DetailExtraValue=el['Category']))
        except TypeError:
            logging.warning("{payload} is null".format(payload=payload))
        except KeyError:
            logging.warning("The field {absent_field} was not added. The available keys are {keys} ".format(
                absent_field=first_key, keys=payload.keys()))
            
    @staticmethod
    def historicize_data_hourly_today():
        print('storicizzo i dati di oggi orari')
        weathers = []
        #cancello eventuali dati di previsioni maggiori di ora gia presenti in storico     
        # now = datetime.utcnow()
        # # Definire il fuso orario italiano
        # italian_timezone = pytz.timezone('Europe/Rome')
        # # Convertire la data e l'ora correnti nel fuso orario italiano
        # now_italian_timezone = now.astimezone(italian_timezone)
        # now_italian_timezone = now_italian_timezone.strftime('%Y-%m-%d %H:%M:%S')
        # with session_scope(__name__) as session:
        #     session.query(Base.classes.weather_hourly_history).filter(Base.classes.weather_hourly_history.DateTime > now_italian_timezone ).delete()
        with session_scope(__name__) as session:
            source_hourly = session.query(Base.classes.weather_hourly).all()
            for item in source_hourly:
                weather=Base.classes.weather_hourly_history(
                                                                    Id=item.Id,
                                                                    InsertDateTime=item.InsertDateTime,
                                                                    DateTime=item.DateTime,
                                                                    Type=item.Type,
                                                                    SiteId=item.SiteId
                                                                    )             
                weathers.append(weather) 
            source_data_hourly = session.query(Base.classes.weather_hourly_data).all()
            for item in source_data_hourly:
                weather=Base.classes.weather_hourly_data_history(
                                                                    Id=item.Id,
                                                                    IdWeather=item.IdWeather,
                                                                    Field=item.Field,
                                                                    Value=item.Value,
                                                                    ValueType=item.ValueType,
                                                                    ValueUnit=item.ValueUnit,
                                                                    ExtraValue=item.ExtraValue
                                                                    )             
                weathers.append(weather) 
            session.add_all(weathers)
    @staticmethod
    def historicize_data_daily_today():
        print('storicizzo i dati di oggi giornalieri')
        weathers = []
        # #cancello eventuali dati di previsioni di oggi gia presenti in storico
        # now = datetime.utcnow()
        # # Definire il fuso orario italiano
        # italian_timezone = pytz.timezone('Europe/Rome')
        # # Convertire la data e l'ora correnti nel fuso orario italiano
        # now_italian_timezone = now.astimezone(italian_timezone)
        
        # now_italian_timezone = now_italian_timezone.replace(hour=7, minute=0, second=0)
        
        # now_italian_timezone = now_italian_timezone.strftime('%Y-%m-%d %H:%M:%S')
        # with session_scope(__name__) as session:
        #     session.query(Base.classes.weather_history).filter(Base.classes.weather_history.DateTime == now_italian_timezone).delete()
        with session_scope(__name__) as session:
            source_data = session.query(Base.classes.weather).all()
            for item in source_data:
                weather=Base.classes.weather_history(
                                                                    Id=item.Id,
                                                                    InsertDateTime=item.InsertDateTime,
                                                                    DateTime=item.DateTime,
                                                                    Type=item.Type,
                                                                    SiteId=item.SiteId
                                                                    )             
                weathers.append(weather) 
            source_data_daily = session.query(Base.classes.weather_daily).all()
            for item in source_data_daily:
                weather=Base.classes.weather_daily_history(
                                                                    Id=item.Id,
                                                                    IdWeather=item.IdWeather,
                                                                    Field=item.Field,
                                                                    Value=item.Value,
                                                                    ValueType=item.ValueType,
                                                                    ValueUnit=item.ValueUnit,
                                                                    ExtraValue=item.ExtraValue
                                                                    )             
                weathers.append(weather) 
            source_data_daily_detail_type = session.query(Base.classes.weather_daily_detail_type).all()
            for item in source_data_daily_detail_type:
                weather=Base.classes.weather_daily_detail_type_history(
                                                                    Id=item.Id,
                                                                    IdWeather=item.IdWeather,
                                                                    Type=item.Type
                                                                    )             
                weathers.append(weather)        
            source_data_daily_detail = session.query(Base.classes.weather_daily_detail).all()
            for item in source_data_daily_detail:
                weather=Base.classes.weather_daily_detail_history(
                                                                    Id=item.Id,
                                                                    IdWeatherDetailType=item.IdWeatherDetailType,
                                                                    DetailType=item.DetailType,
                                                                    Field=item.Field,
                                                                    Value=item.Value,
                                                                    ValueType=item.ValueType,
                                                                    ValueUnit=item.ValueUnit,
                                                                    DetailExtraValue=item.DetailExtraValue
                                                                    ) 
                weathers.append(weather)           
            session.add_all(weathers)

            # print(str(now_italian_timezone))
            # now_italian_timezone = datetime.strptime(now_italian_timezone, '%Y-%m-%d %H:%M:%S')
            # session.query(Base.classes.weather_history).filter(Base.classes.weather_history.DateTime  > now_italian_timezone ).delete()

    @staticmethod       
    def refresh_comunity_from_sqlserver():
            current_directory = os.path.dirname(__file__)  
            json_file_path = os.path.join(current_directory, "customization", "config_db_sqlserver.json")
            with open(json_file_path, "r") as json_file:
                json_data = json.load(json_file)
            driver = json_data["DRIVER"]
            server = json_data["SERVER"]
            database = json_data["DATABASE"]
            uid = json_data["UID"]
            pwd = json_data["PWD"]
            connection_string = f"DRIVER={driver};SERVER={server};DATABASE={database};UID={uid};PWD={pwd}"
            connection = pyodbc.connect(connection_string)
            json_file_path = os.path.join(current_directory,  "customization", "config_apikey.json")
            with open(json_file_path, "r") as json_file:
                json_data = json.load(json_file)
            apikey = json_data["apikey"]
            try:
                query = "SELECT * FROM users"
                cursor = connection.cursor()         
                cursor.execute(query)
                comunities = cursor.fetchall()
                with session_scope(__name__) as session:
                    list_location_keys = session.query(Base.classes.sites).all()
                    session.close()           
                community_ids = [(community.Id,community.Latitudine,community.Longitudine,community.ComunityName,) for community in comunities]
                site_ids = [(site.IdCE,site.Latitude,site.Longitude,site.Name) for site in list_location_keys]
                for common_id in set(community_ids) & set(site_ids):
                    print(f"Utente trovato: {common_id}")
                for community_id in community_ids:
                    if community_id not in site_ids:
                        print(f"Utente senza corrispondenza per: {community_id}")
                        url = "http://dataservice.accuweather.com/locations/v1/cities/geoposition/search"
                        params = {
                            "apikey": apikey,
                            "q": str(community_id[1])+","+str(community_id[2]),
                            "toplevel": "false"
                        }
                        headers = {
                            "Accept": "*/*",
                            "Accept-Encoding": "gzip",
                            "Accept-Language": "it-IT",
                            "Host": "dataservice.accuweather.com",
                            "sec-ch-ua": 'Chromium";v="116',
                            "sec-ch-ua-mobile": "?0",
                            "sec-ch-ua-platform": "Windows",
                            "Sec-Fetch-Dest": "empty",
                            "Sec-Fetch-Mode": "cors",
                            "Sec-Fetch-Site": "cross-site",
                            "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML",
                            "X-Forwarded-For": "79.53.111.84",
                            "X-Forwarded-Port": "443",
                            "X-Forwarded-Proto": "https"
                        }
                        try:
                            response = requests.get(url, params=params, headers=headers)
                            if response.status_code == 200:
                                data = response.json()
                                key_value = data["Key"]
                                print("Valore del campo 'key':", key_value)
                                with session_scope(__name__) as session:
                                    condition = (Base.classes.sites.IdCE == community_id[0]) & (Base.classes.sites.Name == community_id[3])
                                    existing_row = session.query(Base.classes.sites).filter(condition).first()
                                    if existing_row:
                                        update_stmt = update(Base.classes.sites).where(condition).values(
                                            Latitude=community_id[1],
                                            Longitude=community_id[2],
                                            LocationKey=key_value,
                                            ApiKey=apikey
                                        )
                                        session.execute(update_stmt)
                                    else:
                                        new_row = Base.classes.sites(
                                            IdCE=community_id[0],
                                            Name=community_id[3],
                                            Latitude=community_id[1],
                                            Longitude=community_id[2],
                                            LocationKey=key_value,
                                            ApiKey=apikey
                                        )
                                        session.add(new_row)                         
                            else:
                                print(f"Errore nella richiesta HTTP. Codice di stato: {response.status_code}")
                        except Exception as e:
                            print(f"Si è verificato un errore: {str(e)}")
                for site_id in site_ids:
                    if site_id not in community_ids:
                        print(f"Chiave di localizzazione senza corrispondenza per l'ID: {site_id}")
                        rows_to_delete = session.query(Base.classes.sites).filter(Base.classes.sites.IdCE==site_id[0]).filter(Base.classes.sites.Name==site_id[3]).all()
                        for row in rows_to_delete:
                            session.delete(row)
                        session.commit()
            except Exception as e:
                print("Si è verificato un errore:", str(e))
            finally:
                cursor.close()
                connection.close()           
        
    @staticmethod       
    def to_work(now_italian):
        current_directory = os.path.dirname(__file__)  
        json_file_path = os.path.join(current_directory, "customization", "config_db_sqlserver.json")
        with open(json_file_path, "r") as json_file:
            json_data = json.load(json_file)
        driver = json_data["DRIVER"]
        server = json_data["SERVER"]
        database = json_data["DATABASE"]
        uid = json_data["UID"]
        pwd = json_data["PWD"]
        connection_string = f"DRIVER={driver};SERVER={server};DATABASE={database};UID={uid};PWD={pwd}"
        connection = pyodbc.connect(connection_string)       
        try:
            query = "SELECT * FROM Jobs where Job='Meteo'"
            cursor = connection.cursor()         
            cursor.execute(query)
            Jobs = cursor.fetchall() 
            for job in Jobs:
                if(job.IsActive):
                    now = datetime.utcnow()     
                    italian_timezone = pytz.timezone('Europe/Rome')
                    current_datetime = now.astimezone(italian_timezone)
                    data_odierna = current_datetime.date()
                    hour=0
                    minute=0
                    if job.HourToStart!=None:
                        hour= job.HourToStart
                    if job.MinuteToStart!=None:
                        minute= job.MinuteToStart
                    data_attivazione = datetime.combine(data_odierna,time(hour, minute, 0))  
                    if(job.LastTimestampStart):           
                        if   current_datetime.date()>job.LastTimestampStart.date() and current_datetime.timestamp()>data_attivazione.timestamp():
                            return True;
                        else: return False;
                    else:
                        if   current_datetime.timestamp()>=data_attivazione.timestamp():
                            return True;
                        else: return False;
                else:
                    return False;
        except Exception as e:
            print(str(e))
            return False;
        finally:
            cursor.close()
            connection.close() 
        return False;

    @staticmethod       
    def start_job():
        try:    
            now = datetime.utcnow()     
            italian_timezone = pytz.timezone('Europe/Rome')
            now_italian_timezone = now.astimezone(italian_timezone)
            now_italian_timezone = now_italian_timezone.strftime('%Y-%m-%d %H:%M:%S')
            current_directory = os.path.dirname(__file__)  
            json_file_path = os.path.join(current_directory, "customization", "config_db_sqlserver.json")
            with open(json_file_path, "r") as json_file:
                json_data = json.load(json_file)
            driver = json_data["DRIVER"]
            server = json_data["SERVER"]
            database = json_data["DATABASE"]
            uid = json_data["UID"]
            pwd = json_data["PWD"]
            connection_string = f"DRIVER={driver};SERVER={server};DATABASE={database};UID={uid};PWD={pwd}"
            connection = pyodbc.connect(connection_string)          
            if bool==True:
                query = f"UPDATE [dbo].[Jobs] SET [LastTimestampStart] = '{now_italian_timezone}' WHERE Job='Meteo'"
            else:
                query = f"UPDATE [dbo].[Jobs] SET [LastTimestampStart] = '{now_italian_timezone}' WHERE Job='Meteo'"
            cursor = connection.cursor()         
            cursor.execute(query)
            connection.commit()
        except Exception as e:
            print(str(e))
        finally:
            cursor.close()
            connection.close() 
        
    @staticmethod       
    def refresh_status(bool):
        try:    
            now = datetime.utcnow()     
            italian_timezone = pytz.timezone('Europe/Rome')
            now_italian_timezone = now.astimezone(italian_timezone)
            now_italian_timezone = now_italian_timezone.strftime('%Y-%m-%d %H:%M:%S')
            current_directory = os.path.dirname(__file__)  
            json_file_path = os.path.join(current_directory, "customization", "config_db_sqlserver.json")
            with open(json_file_path, "r") as json_file:
                json_data = json.load(json_file)
            driver = json_data["DRIVER"]
            server = json_data["SERVER"]
            database = json_data["DATABASE"]
            uid = json_data["UID"]
            pwd = json_data["PWD"]
            connection_string = f"DRIVER={driver};SERVER={server};DATABASE={database};UID={uid};PWD={pwd}"
            connection = pyodbc.connect(connection_string)          
            if bool==True:
                query = f"UPDATE [dbo].[Jobs] SET [LastTimestampEnd] = '{now_italian_timezone}',[Errors] = {1} WHERE Job='Meteo'"
            else:
                query = f"UPDATE [dbo].[Jobs] SET [LastTimestampEnd] = '{now_italian_timezone}',[Errors] = {0} WHERE Job='Meteo'"
            cursor = connection.cursor()         
            cursor.execute(query)
            connection.commit()
        except Exception as e:
            print(str(e))
        finally:
            cursor.close()
            connection.close() 
        
    @staticmethod
    def run():
        try:
            print('start')
            while True:
                now = datetime.utcnow()     
                italian_timezone = pytz.timezone('Europe/Rome')
                now_italian = now.astimezone(italian_timezone)
                now_italian_timezone = now_italian.strftime('%Y-%m-%d %H:%M:%S')
                if Weather.to_work(now_italian):
                        Weather.start_job()    
                        Weather.refresh_comunity_from_sqlserver()      
                        with session_scope(__name__) as session:
                            list_location_keys = session.query(Base.classes.sites).all()
                            session.close()
                        weathers = []
                        for lk in list_location_keys: 
                            weather = Base.classes.weather_hourly(InsertDateTime=now_italian_timezone,
                                                                    DateTime=now_italian_timezone, Type="HOURLY")

                            url2 = Weather.api_hourly_12h.format(**{"location_key": str(lk.LocationKey),
                                                                        "api_key": lk.ApiKey})
                            print(url2)
                            content_api_hourly_12h = json.loads(requests.get(url2).content)             
                            # with session_scope(__name__) as session:
                            #     weather_daily_list = []
                            #     weather_daily_list.append(Base.classes.weather_daily(Field="SunRise"))
                            #     weather_daily_list.append(Base.classes.weather_daily(Field="SunSet"))
                            #     weather = Base.classes.weather(InsertDateTime=datetime.datetime.utcnow(), DateTime=datetime.datetime.utcnow(),
                            #                                      Type="DAILY", weather_daily_collection = weather_daily_list)
                            #     session.add(weather)

                            # payload = (content_api_daily_1day["DailyWeathers"])[0]
                            # payload = (content_api_daily_5day["DailyWeathers"])
                            for payload in content_api_hourly_12h:
                                weather_hourly_list = []
                                Weather.append_entries_one1(payload, weather_hourly_list, "Temperature")
                                Weather.append_entries_one1(payload, weather_hourly_list, "SolarIrradiance")
                                Weather.append_entries_one1(payload, weather_hourly_list, "RealFeelTemperature")
                                Weather.append_entries_one1(payload, weather_hourly_list, "RealFeelTemperatureShade")
                                Weather.append_entries_one1(payload, weather_hourly_list, "Rain")
                                Weather.append_entries_one1(payload, weather_hourly_list, "TotalLiquid")
                                Weather.append_entries_one1(payload, weather_hourly_list, "Snow")
                                Weather.append_entries_one1(payload, weather_hourly_list, "Ice")
                                Weather.append_entries_two1(payload, weather_hourly_list, "Wind", "Speed")
                                Weather.append_entries_two1(payload, weather_hourly_list, "WindGust", "Speed")
                                Weather.append_entries_three1(payload, weather_hourly_list, 'PrecipitationProbability')
                                Weather.append_entries_three1(payload, weather_hourly_list, 'ThunderstormProbability')
                                Weather.append_entries_three1(payload, weather_hourly_list, 'RainProbability')
                                Weather.append_entries_three1(payload, weather_hourly_list, 'SnowProbability')
                                Weather.append_entries_three1(payload, weather_hourly_list, 'IceProbability')
                                weather = Base.classes.weather_hourly(InsertDateTime=now_italian_timezone, SiteId = lk.Id,
                                                                DateTime= datetime.fromisoformat(payload['DateTime']),
                                                                Type="HOURLY", weather_hourly_data_collection=weather_hourly_list)
                                weathers.append(weather)
                        with session_scope(__name__) as session:
                            session.query(Base.classes.weather_hourly_data).delete()
                            session.query(Base.classes.weather_hourly).delete()
                        with session_scope(__name__) as session:
                            session.add_all(weathers)      
                        Weather.historicize_data_hourly_today()
                        print('fatto ORARIO per il giorno '+str(now_italian_timezone)+'\n')
                        print('run weather main DAILY\n')
                        with session_scope(__name__) as session:
                            list_location_keys = session.query(Base.classes.sites).all()
                            session.close()
                        weathers = []
                        for lk in list_location_keys:
                            url4 = Weather.api_daily_5day.format(**{"location_key": str(lk.LocationKey),"api_key": lk.ApiKey})
                            print(url4)
                            content_api_daily_5day = json.loads(requests.get(url4).content)
                            for payload in content_api_daily_5day["DailyWeathers"]:
                                weather_daily_list = []
                                weather_daily_detail_list_day = []
                                weather_daily_detail_list_night = []
                                weather_daily_detail_list_pollen = []
                                Weather.append_entries_one(payload, weather_daily_list, "Sun", "Rise")
                                Weather.append_entries_one(payload, weather_daily_list, "Sun", "Set")
                                Weather.append_entries_one(payload, weather_daily_list, "Moon", "Rise")
                                Weather.append_entries_one(payload, weather_daily_list, "Moon", "Set")
                                Weather.append_entries_one(payload, weather_daily_list, "Moon", "Phase")
                                Weather.append_entries_two(payload, weather_daily_list, "Temperature", "Minimum")
                                Weather.append_entries_two(payload, weather_daily_list, "Temperature", "Maximum")
                                Weather.append_entries_three(payload, weather_daily_list, "RealFeelTemperature", "Minimum")
                                Weather.append_entries_three(payload, weather_daily_list, "RealFeelTemperature", "Maximum")
                                Weather.append_entries_three(payload, weather_daily_list, "RealFeelTemperatureShade", "Minimum")
                                Weather.append_entries_three(payload, weather_daily_list, "RealFeelTemperatureShade", "Maximum")
                                Weather.append_entries_four(payload, weather_daily_list, "HoursOfSun")
                                Weather.append_entries_two(payload, weather_daily_list, "DegreeDaySummary", "Heating")
                                Weather.append_entries_two(payload, weather_daily_list, "DegreeDaySummary", "Cooling")
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day','Icon')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day', 'IconPhrase')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day','ShortPhrase')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day', 'LongPhrase')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day','PrecipitationProbability')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day','ThunderstormProbability')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day', 'RainProbability')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day', 'SnowProbability')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day','IceProbability')
                                Weather.append_entries_day_night_three(payload, weather_daily_detail_list_day, 'Day', 'Wind', 'Speed')
                                Weather.append_entries_day_night_four(payload, weather_daily_detail_list_day, 'Day', 'Wind', 'Direction')
                                Weather.append_entries_day_night_three(payload, weather_daily_detail_list_day, 'Day', 'WindGust', 'Speed')
                                Weather.append_entries_day_night_four(payload, weather_daily_detail_list_day, 'Day', 'WindGust', 'Direction')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_day, 'Day','TotalLiquid')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_day, 'Day','Rain')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_day, 'Day','Snow')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_day, 'Day','Ice')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day', 'HoursOfPrecipitation')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day','HoursOfRain')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day', 'HoursOfSnow')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day','HoursOfIce')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_day, 'Day','CloudCover')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_day, 'Day','Evapotranspiration')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_day, 'Day', 'SolarIrradiance')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','Icon')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night', 'IconPhrase')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','ShortPhrase')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night', 'LongPhrase')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','PrecipitationProbability')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','ThunderstormProbability')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','RainProbability')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night', 'SnowProbability')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','IceProbability')
                                Weather.append_entries_day_night_three(payload, weather_daily_detail_list_night, 'Night', 'Wind', 'Speed')
                                Weather.append_entries_day_night_four(payload, weather_daily_detail_list_night, 'Night', 'Wind', 'Direction')
                                Weather.append_entries_day_night_three(payload, weather_daily_detail_list_night, 'Night', 'WindGust', 'Speed')
                                Weather.append_entries_day_night_four(payload, weather_daily_detail_list_night, 'Night', 'WindGust', 'Direction')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_night, 'Night','TotalLiquid')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_night, 'Night','Rain')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_night, 'Night','Snow')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_night, 'Night','Ice')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','HoursOfPrecipitation')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','HoursOfRain')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','HoursOfSnow')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','HoursOfIce')
                                Weather.append_entries_day_night_two(payload, weather_daily_detail_list_night, 'Night','CloudCover')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_night, 'Night','Evapotranspiration')
                                Weather.append_entries_day_night_one(payload, weather_daily_detail_list_night, 'Night','SolarIrradiance')
                                Weather.append_entries_pollen(payload, weather_daily_detail_list_pollen, 'AirAndPollen')
                                weather_daily_detail_type_day = Base.classes.weather_daily_detail_type(Type="DAY",weather_daily_detail_collection=weather_daily_detail_list_day)
                                weather_daily_detail_type_night = Base.classes.weather_daily_detail_type(
                                    weather_daily_detail_collection=weather_daily_detail_list_night,
                                    Type="NIGHT")
                                weather_daily_detail_type_pollen = Base.classes.weather_daily_detail_type(
                                    weather_daily_detail_collection=weather_daily_detail_list_pollen,
                                    Type="POLLEN")
                                weather_daily_detail_type_list = [weather_daily_detail_type_day, weather_daily_detail_type_night,
                                                                weather_daily_detail_type_pollen]
                                weather = Base.classes.weather(InsertDateTime=now_italian_timezone, SiteId = lk.Id,
                                                                DateTime=datetime.fromisoformat(payload['Date']),
                                                                Type="DAILY", weather_daily_collection=weather_daily_list,
                                                                weather_daily_detail_type_collection=weather_daily_detail_type_list)
                                weathers.append(weather)
                        with session_scope(__name__) as session:
                            session.query(Base.classes.weather_daily_detail).delete()
                            session.query(Base.classes.weather_daily_detail_type).delete()
                            session.query(Base.classes.weather_daily).delete()
                            session.query(Base.classes.weather).delete()
                        with session_scope(__name__) as session:
                            session.add_all(weathers)   
                        Weather.historicize_data_daily_today()
                        print('fatto per il giorno '+str(now_italian_timezone)+'\n')          
                        with session_scope(__name__) as session:
                            now1 = datetime.utcnow()
                            now_italian_timezone1 = now1.astimezone(italian_timezone)
                            now_italian_timezone1 = now_italian_timezone1.strftime('%Y-%m-%d %H:%M:%S')
                            print('avvio store procedure di pulizia '+str(now_italian_timezone1) +'\n')
                            session.execute("CALL clean_duplicate()")
                            now2 = datetime.utcnow()
                            now_italian_timezone2 = now2.astimezone(italian_timezone)
                            now_italian_timezone2 = now_italian_timezone2.strftime('%Y-%m-%d %H:%M:%S')      
                            print('fine store procedure di pulizia '+str(now_italian_timezone2)+'\n')
                        Weather.refresh_status(False)
                else:
                     print("Non devo lavorare")
                time.sleep(5)
        except Exception as e:
            print("Non è stato posssibile completare "+ str(e))
            Weather.refresh_status(True)
if __name__ == '__main__':
    Weather.run()
