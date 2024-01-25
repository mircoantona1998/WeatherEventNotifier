from prometheus_api_client import PrometheusConnect

from Configurations.Configurations import Configurations

class Prometheus:
    def get_metric_value(query):
        prometheus_url="http://"+Configurations().prometheus_ip+":9090"
        prom = PrometheusConnect(url=prometheus_url)
        return prom.custom_query(query)

