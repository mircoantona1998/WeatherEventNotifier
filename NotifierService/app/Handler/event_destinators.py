from Configurations.Configurations import Configurations
from Utils.Json import Json


class GestoreDestinatari:

    def determina_destinatario(self, header_creator):
        if header_creator == "ExposeAPIService":
            return Configurations().topic_to_userdata
        elif header_creator == "SchedulerService":
            return Configurations().topic_to_scheduler
        elif header_creator == "NotifierService":
            return Configurations().topic_to_notifier
        elif header_creator == "WeatherService":
            return Configurations().topic_to_weather
        elif header_creator == "ConfiguratorService":
            return Configurations().topic_to_configuration
        elif header_creator == "MailService":
            return Configurations().topic_to_mail
        elif header_creator == "TelegramService":
            return Configurations().topic_to_telegram
        else:
            raise Exception("Non esiste mapping con il creator")

