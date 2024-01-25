from datetime import datetime


class MetricData:
    def __init__(self, Metric_name=None, Action=None, Code=None, Controller=None, Endpoint=None, Instance=None, Job=None, Method=None, Value1=None, Value2=None):
        self.Metric_name = Metric_name
        self.Action = Action
        self.Code = Code
        self.Controller = Controller
        self.Endpoint = Endpoint
        self.Instance = Instance
        self.Job = Job
        self.Method = Method
        self.Value1 = Value1
        self.Value2 = Value2
        self.Timestamp= datetime.utcnow()