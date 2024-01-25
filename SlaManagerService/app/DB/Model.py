# models.py

from sqlalchemy import Column, DATETIME, BOOLEAN, INTEGER, VARCHAR, NVARCHAR, FLOAT
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()

class Heartbeat(Base):
    __tablename__ = "Heartbeat"
    Id = Column(INTEGER, primary_key=True)
    IdService = Column(INTEGER, nullable=False)
    Timestamp = Column(DATETIME, nullable=True)


class Services(Base):
    __tablename__ = "Services"
    Id = Column(INTEGER, primary_key=True)
    Service = Column(VARCHAR(50), nullable=False)
    Servicename = Column(VARCHAR(50), nullable=True)
    Password = Column(VARCHAR(50), nullable=True)  
    isActive = Column(BOOLEAN, nullable=True)
    DateUpdate = Column(DATETIME, nullable=True)
    isBlocked = Column(INTEGER, nullable=True)
    Partition = Column(INTEGER, nullable=True)


class Messagereceived(Base):
    __tablename__ = "MessageReceived"
    Id = Column(INTEGER, primary_key=True)
    message = Column(NVARCHAR, nullable=True)
    offset = Column(INTEGER, nullable=True)
    timestamp = Column(DATETIME, nullable=True)
    type = Column(VARCHAR(50), nullable=True)
    idOffsetResponse = Column(INTEGER, nullable=True)
    tagMessage = Column(VARCHAR(50), nullable=True)
    topic = Column(VARCHAR(50), nullable=True)
    creator = Column(VARCHAR(200), nullable=True)
    code = Column(VARCHAR(20), nullable=True)
    partition = Column(INTEGER, nullable=True)


class Messagesent(Base):
    __tablename__ = "MessageSent"
    Id = Column(INTEGER, primary_key=True)
    message = Column(NVARCHAR, nullable=True)
    offset = Column(INTEGER, nullable=True)
    timestamp = Column(DATETIME, nullable=True)
    type = Column(VARCHAR(50), nullable=True)
    idOffsetResponse = Column(INTEGER, nullable=True)
    tagMessage = Column(VARCHAR(50), nullable=True)
    topic = Column(VARCHAR(50), nullable=True)
    creator = Column(VARCHAR(200), nullable=True)
    code = Column(VARCHAR(20), nullable=True)
    partition = Column(INTEGER, nullable=True)


class Metricdata(Base):
    __tablename__ = "MetricData"
    Id = Column(INTEGER, primary_key=True)
    Metric_name = Column(VARCHAR(255), nullable=True)
    Action = Column(VARCHAR(255), nullable=True)
    Code = Column(VARCHAR(255), nullable=True)
    Controller = Column(VARCHAR(255), nullable=True)
    Endpoint = Column(VARCHAR(255), nullable=True)
    Instance = Column(VARCHAR(255), nullable=True)
    Job = Column(VARCHAR(255), nullable=True)
    Method = Column(VARCHAR(255), nullable=True)
    Value1 = Column(FLOAT, nullable=True)
    Value2 = Column(VARCHAR(255), nullable=True)
    Timestamp = Column(DATETIME, nullable=True)

class Monitoringmetric(Base):
    __tablename__ = "MonitoringMetric"
    Id = Column(INTEGER, primary_key=True)
    Metric = Column(VARCHAR(100), nullable=True)
    Description = Column(VARCHAR(255), nullable=True)


class Sla(Base):
    __tablename__ = "Sla"
    Id = Column(INTEGER, primary_key=True)
    IdMonitoringMetric = Column(INTEGER, nullable=True)
    FromDesiredValue = Column(FLOAT)
    ToDesiredValue = Column(FLOAT)
    UpdateDatetime = Column(DATETIME, nullable=True)


class Slametricstatus(Base):
    __tablename__ = "SlaMetricStatus"
    Id = Column(INTEGER, primary_key=True)
    IdSla = Column(INTEGER, nullable=True)
    IdStatus = Column(INTEGER, nullable=True)
    FromDesiredValue = Column(FLOAT)
    ToDesiredValue = Column(FLOAT)
    MisuredValue = Column(FLOAT)
    Datetime = Column(DATETIME, nullable=True)
    Action = Column(VARCHAR(255), nullable=True)
    Code = Column(VARCHAR(255), nullable=True)
    Controller = Column(VARCHAR(255), nullable=True)
    Endpoint = Column(VARCHAR(255), nullable=True)
    Instance = Column(VARCHAR(255), nullable=True)
    Job = Column(VARCHAR(255), nullable=True)
    Method = Column(VARCHAR(255), nullable=True)


class Slametricviolation(Base):
    __tablename__ = "SlaMetricViolation"
    Id = Column(INTEGER, primary_key=True)
    IdSla = Column(INTEGER, nullable=True)
    Violation = Column(VARCHAR(255), nullable=True)
    FromDesiredValue = Column(FLOAT)
    ToDesiredValue = Column(FLOAT)
    MisuredValue = Column(FLOAT)
    Datetime = Column(DATETIME, nullable=True)
    Action = Column(VARCHAR(255), nullable=True)
    Code = Column(VARCHAR(255), nullable=True)
    Controller = Column(VARCHAR(255), nullable=True)
    Endpoint = Column(VARCHAR(255), nullable=True)
    Instance = Column(VARCHAR(255), nullable=True)
    Job = Column(VARCHAR(255), nullable=True)
    Method = Column(VARCHAR(255), nullable=True)


class Slametricviolationforecast(Base):
    __tablename__ = "SlaMetricViolationForecast"
    Id = Column(INTEGER, primary_key=True)
    IdSla = Column(INTEGER, nullable=True)
    Violation = Column(VARCHAR(255), nullable=True)
    FromDesiredValue = Column(FLOAT)
    ToDesiredValue = Column(FLOAT)
    MisuredValue = Column(FLOAT)
    Datetime = Column(DATETIME, nullable=True)
    Action = Column(VARCHAR(255), nullable=True)
    Code = Column(VARCHAR(255), nullable=True)
    Controller = Column(VARCHAR(255), nullable=True)
    Endpoint = Column(VARCHAR(255), nullable=True)
    Instance = Column(VARCHAR(255), nullable=True)
    Job = Column(VARCHAR(255), nullable=True)
    Method = Column(VARCHAR(255), nullable=True)


class Status(Base):
    __tablename__ = "Status"
    Id = Column(INTEGER, primary_key=True)
    Code = Column(VARCHAR(50), nullable=True)
    Description = Column(VARCHAR(255), nullable=True)


class Users(Base):
    __tablename__ = "Users"
    Id = Column(INTEGER, primary_key=True)
    Nome = Column(VARCHAR(50), nullable=True)
    Cognome = Column(VARCHAR(50), nullable=True)
    Username = Column(VARCHAR(50), nullable=True)
    Password = Column(VARCHAR(50), nullable=True)
    isActive = Column(BOOLEAN, nullable=True)
    DateUpdate = Column(DATETIME, nullable=True)
    Address = Column(VARCHAR(50), nullable=True)
    Cap = Column(VARCHAR(50), nullable=True)
    City = Column(VARCHAR(50), nullable=True)
    LastAccess = Column(DATETIME, nullable=True)
    isBlocked = Column(INTEGER, nullable=True)
    Partition = Column(INTEGER, nullable=True)

class SlaView(Base):
    __tablename__ = 'Sla_view'

    Id = Column(INTEGER, primary_key=True)
    IdMonitoringMetric = Column(INTEGER)
    FromDesiredValue = Column(FLOAT)
    ToDesiredValue = Column(FLOAT)
    UpdateDatetime = Column(DATETIME)
    Metric = Column(VARCHAR(255))
    Description = Column(VARCHAR(255))


class SlaMetricStatusView(Base):
    __tablename__ = 'Sla_metric_status_view'

    IdSla = Column(INTEGER, primary_key=True)
    FromDesiredValue = Column(FLOAT)
    ToDesiredValue = Column(FLOAT)
    MisuredValue = Column(FLOAT)
    Metric = Column(VARCHAR(255))
    MetricDescription = Column(VARCHAR(255))
    StatusCode = Column(VARCHAR(50))
    StatusDescription = Column(VARCHAR(255))
    datetime = Column(DATETIME)
    Action = Column(VARCHAR(255))
    Code = Column(VARCHAR(255))
    Controller = Column(VARCHAR(255))
    Endpoint = Column(VARCHAR(255))
    Instance = Column(VARCHAR(255))
    Job = Column(VARCHAR(255))
    Method = Column(VARCHAR(255))


class SlaMetricViolationView(Base):
    __tablename__ = 'Sla_metric_violation_view'

    IdSla = Column(INTEGER, primary_key=True)
    FromDesiredValue = Column(FLOAT)
    ToDesiredValue = Column(FLOAT)
    MisuredValue = Column(FLOAT)
    Violation = Column(VARCHAR(255))
    Datetime = Column(DATETIME)
    Metric = Column(VARCHAR(255))
    MetricDescription = Column(VARCHAR(255))


class SlaMetricViolationForecastView(Base):
    __tablename__ = 'Sla_metric_violation_forecast_view'

    IdSla = Column(INTEGER, primary_key=True)
    FromDesiredValue = Column(FLOAT)
    ToDesiredValue = Column(FLOAT)
    MisuredValue = Column(FLOAT)
    Violation = Column(VARCHAR(255))
    Datetime = Column(DATETIME)
    Metric = Column(VARCHAR(255))
    MetricDescription = Column(VARCHAR(255))
