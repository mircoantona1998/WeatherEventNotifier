class Heartbeat(Base):
    __tablename__ = "Heartbeat"

    IdService = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Timestamp = Column(DATETIME, nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Services(Base):
    __tablename__ = "Services"

    Service = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8D84AC0>, for_update=False), primary_key=False, unique=None, index=None)
    Servicename = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8D845E0>, for_update=False), primary_key=False, unique=None, index=None)
    Password = Column(NVARCHAR COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8D84CD0>, for_update=False), primary_key=False, unique=None, index=None)
    isActive = Column(BIT, nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8D84F70>, for_update=False), primary_key=False, unique=None, index=None)
    DateUpdate = Column(DATETIME, nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8D84D60>, for_update=False), primary_key=False, unique=None, index=None)
    isBlocked = Column(INTEGER, nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8D84AF0>, for_update=False), primary_key=False, unique=None, index=None)
    Partition = Column(INTEGER, nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8D94280>, for_update=False), primary_key=False, unique=None, index=None)


class Messagereceived(Base):
    __tablename__ = "MessageReceived"

    message = Column(NVARCHAR COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    offset = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    timestamp = Column(DATETIME, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    type = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    idOffsetResponse = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    tagMessage = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    topic = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    creator = Column(VARCHAR(200) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    code = Column(VARCHAR(20) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    partition = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Messagesent(Base):
    __tablename__ = "MessageSent"

    message = Column(NVARCHAR COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    offset = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    timestamp = Column(DATETIME, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    type = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    idOffsetResponse = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    tagMessage = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    topic = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    creator = Column(VARCHAR(200) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    code = Column(VARCHAR(20) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    partition = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Metricdata(Base):
    __tablename__ = "MetricData"

    Metric_name = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Action = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Code = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Controller = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Endpoint = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Instance = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Job = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Method = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Value1 = Column(FLOAT, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Value2 = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Monitoringmetric(Base):
    __tablename__ = "MonitoringMetric"

    Metric = Column(VARCHAR(100) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Description = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Sla(Base):
    __tablename__ = "Sla"

    IdMonitoringMetric = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Partition = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Symbol = Column(VARCHAR(2) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Value = Column(FLOAT, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    UpdateDatetime = Column(DATETIME, nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Slametricstatus(Base):
    __tablename__ = "SlaMetricStatus"

    IdSla = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    IdStatus = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Datetime = Column(DATETIME, nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Slametricviolation(Base):
    __tablename__ = "SlaMetricViolation"

    IdSla = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Violation = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Datetime = Column(DATETIME, nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Slametricviolationforecast(Base):
    __tablename__ = "SlaMetricViolationForecast"

    IdSla = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Violation = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Datetime = Column(DATETIME, nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Status(Base):
    __tablename__ = "Status"

    Code = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Description = Column(VARCHAR(255) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)


class Users(Base):
    __tablename__ = "Users"

    Nome = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Cognome = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8DC8760>, for_update=False), primary_key=False, unique=None, index=None)
    Username = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Password = Column(NVARCHAR COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    isActive = Column(BIT, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    DateUpdate = Column(DATETIME, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Address = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8DC82B0>, for_update=False), primary_key=False, unique=None, index=None)
    Cap = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8DC8730>, for_update=False), primary_key=False, unique=None, index=None)
    City = Column(VARCHAR(50) COLLATE "SQL_Latin1_General_CP1_CI_AS", nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8DC8E50>, for_update=False), primary_key=False, unique=None, index=None)
    LastAccess = Column(DATETIME, nullable=True, server_default=DefaultClause(<sqlalchemy.sql.elements.TextClause object at 0x00000125D8DC8490>, for_update=False), primary_key=False, unique=None, index=None)
    isBlocked = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)
    Partition = Column(INTEGER, nullable=True, server_default=None, primary_key=False, unique=None, index=None)


