from time import sleep
from Configurations.Configurations import Configurations
from DB.Repository.MonitoringMetricRepo import MonitoringMetricRepo
from DB.Repository.SlaMetricStatusRepo import SlaMetricStatusRepo
from DB.Repository.SlaMetricViolationForecastRepo import SlaMetricViolationForecastRepo
from DB.Repository.SlaMetricViolationRepo import SlaMetricViolationRepo
from DB.Repository.MetricDataRepo import MetricDataRepo
from DB.Repository.SlaRepo import SlaRepo
from DB.Repository.StatusRepo import StatusRepo
from Model.MetricData import MetricData
from Model.SlaMetricStatus import SlaMetricStatus
from Model.SlaMetricViolation import SlaMetricViolation
from Utils.Logger import Logger
import inspect
from datetime import datetime
from DB.Session import Session
from Utils.Prometheus import Prometheus

if __name__ == "__main__":
    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - avvio Sla manager service  - {inspect.currentframe().f_globals['__file__']}")
    Configurations()
    Session.wait_for_sql_server()
    while True:
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - repeat start  - {inspect.currentframe().f_globals['__file__']}")
        metrics=MonitoringMetricRepo.get_all()
        for metric in metrics:
            datas = Prometheus.get_metric_value(metric["Metric"])
            for data in datas:
                try:
                    metric_name = metric["Metric"]
                except:
                    metric_name =None
                try:
                    instance = data['metric']['instance']
                except:
                    instance =None
                try:
                    job = data['metric']['job']
                except:
                    job =None
                try:
                    value1, value2 = data['value']
                except:
                   value1=None
                   value2=None
                try:
                    action = data['metric']['action']
                except:
                    action = None
                try:
                    controller = data['metric']['controller']
                except:
                    controller =None
                try:
                    endpoint = data['metric']['endpoint']
                except:
                    endpoint = None
                try:
                    method = data['metric']['method']
                except:
                    method =None
                try:
                    code = data['metric']['code']
                except:
                    code = None
                metric_data = MetricData(Metric_name=metric_name,Action=action,Code=code,Controller=controller,Endpoint=endpoint,Method=method, Instance= instance, Job=job, Value1=value1, Value2=value2)
                MetricDataRepo.add_element(metric_data)
                sla=SlaRepo.get_by_id_metric(metric["Id"])
                if sla is not None:
                    if value2 is not None:
                        if sla["Symbol"]==">":
                            if float(value2)>float(sla["DesiredValue"]):
                                statusOk=StatusRepo.get_by_code("OK")
                                slaMetricStatus=SlaMetricStatus(sla["Id"],statusOk["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                            else:
                                statusError=StatusRepo.get_by_code("KO")
                                slaMetricStatus= SlaMetricStatus(sla["Id"],statusError["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                slaMetricViolation= SlaMetricViolation(sla["Id"],"VIOLAZIONE",action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                                SlaMetricViolationRepo.add_element(slaMetricViolation)
                        elif sla["Symbol"]=="<" :
                            if float(value2)<float(sla["DesiredValue"]):
                                statusOk=StatusRepo.get_by_code("OK")
                                slaMetricStatus=SlaMetricStatus(sla["Id"],statusOk["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                            else:
                                statusError=StatusRepo.get_by_code("KO")
                                slaMetricStatus= SlaMetricStatus(sla["Id"],statusError["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                slaMetricViolation= SlaMetricViolation(sla["Id"],"VIOLAZIONE",action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                                SlaMetricViolationRepo.add_element(slaMetricViolation)
                        elif sla["Symbol"]=="<=" :
                            if float(value2)<=float(sla["DesiredValue"]):
                                statusOk=StatusRepo.get_by_code("OK")
                                slaMetricStatus=SlaMetricStatus(sla["Id"],statusOk["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                            else:
                                statusError=StatusRepo.get_by_code("KO")
                                slaMetricStatus= SlaMetricStatus(sla["Id"],statusError["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                slaMetricViolation= SlaMetricViolation(sla["Id"],"VIOLAZIONE",action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                                SlaMetricViolationRepo.add_element(slaMetricViolation)
                        elif sla["Symbol"]==">" :
                            if float(value2)>float(sla["DesiredValue"]):
                                statusOk=StatusRepo.get_by_code("OK")
                                slaMetricStatus=SlaMetricStatus(sla["Id"],statusOk["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                            else:
                                statusError=StatusRepo.get_by_code("KO")
                                slaMetricStatus= SlaMetricStatus(sla["Id"],statusError["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                slaMetricViolation= SlaMetricViolation(sla["Id"],"VIOLAZIONE",action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                                SlaMetricViolationRepo.add_element(slaMetricViolation)
                        elif sla["Symbol"]==">=" :
                            if float(value2)>=float(sla["DesiredValue"]):
                                statusOk=StatusRepo.get_by_code("OK")
                                slaMetricStatus=SlaMetricStatus(sla["Id"],statusOk["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                            else:
                                statusError=StatusRepo.get_by_code("KO")
                                slaMetricStatus= SlaMetricStatus(sla["Id"],statusError["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                slaMetricViolation= SlaMetricViolation(sla["Id"],"VIOLAZIONE",action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                                SlaMetricViolationRepo.add_element(slaMetricViolation)
                        elif sla["Symbol"]=="==" :
                            if float(value2)==float(sla["DesiredValue"]):
                                statusOk=StatusRepo.get_by_code("OK")
                                slaMetricStatus=SlaMetricStatus(sla["Id"],statusOk["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                            else:
                                statusError=StatusRepo.get_by_code("KO")
                                slaMetricStatus= SlaMetricStatus(sla["Id"],statusError["Id"],action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                slaMetricViolation= SlaMetricViolation(sla["Id"],"VIOLAZIONE",action,code,controller,endpoint,instance,job,method,float(sla["DesiredValue"]),float(value2))
                                SlaMetricStatusRepo.patch_element(slaMetricStatus)
                                SlaMetricViolationRepo.add_element(slaMetricViolation)
        sleep(10)
    

