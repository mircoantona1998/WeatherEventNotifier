from Utils.Json import Json


class GestoreDestinatari:
    def __init__(self):
        self.configurazioni = Json.leggi_configurazioni

    def determina_destinatario(self, header_creator):
        if header_creator == "ExposeAPIService":
            return self.configurazioni("topic_to_userdata")
        elif header_creator == "NotifierService":
            return self.configurazioni("topic_to_notifier")
        elif header_creator == "WeatherService":
            return self.configurazioni("topic_to_weather")
        elif header_creator == "ConfiguratorService":
            return self.configurazioni("topic_to_configuration")
        elif header_creator == "MailService":
            return self.configurazioni("topic_to_mail")
        elif header_creator == "TelegramService":
            return self.configurazioni("topic_to_telegram")
        elif header_creator == "TipService":
            return self.configurazioni("topic_to_tip")
        else:
            raise Exception("Non esiste mapping con il creator")

