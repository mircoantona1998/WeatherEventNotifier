from datetime import datetime


class SlaMetricViolation:
    def __init__(self, IdSla=None, Violation=None,Action=None,Code=None,Controller=None,Endpoint=None,Instance=None,Job=None,Method=None,DesiredValue=None,MisuredValue=None):
        self.IdSla = IdSla
        self.Violation = Violation
        self.DesiredValue = DesiredValue
        self.MisuredValue = MisuredValue 
        self.Datetime = datetime.utcnow()
        self.Action = Action
        self.Code = Code
        self.Controller = Controller
        self.Endpoint = Endpoint
        self.Instance = Instance
        self.Job = Job
        self.Method = Method