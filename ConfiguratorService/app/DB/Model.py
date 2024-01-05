# coding: utf-8
from sqlalchemy import CHAR, Column, DateTime, Float, ForeignKey, Integer, String, Table, text
from sqlalchemy.dialects.mysql import LONGTEXT, TINYINT
from sqlalchemy.orm import relationship
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
metadata = Base.metadata

class Frequency(Base):
    __tablename__ = 'Frequency'

    Id = Column(Integer, primary_key=True)
    FrequencyName = Column(String(100))
    Minutes = Column(Integer)
    IsActive = Column(TINYINT(1))


class MessageReceived(Base):
    __tablename__ = 'MessageReceived'

    id = Column(Integer, primary_key=True)
    message = Column(LONGTEXT)
    offset = Column(Integer)
    timestamp = Column(DateTime)
    type = Column(String(50))
    idOffsetResponse = Column(Integer)
    tagMessage = Column(String(50))
    topic = Column(String(50))
    creator = Column(String(500))
    code = Column(String(20))
    partition = Column(Integer)


class MessageSent(Base):
    __tablename__ = 'MessageSent'

    id = Column(Integer, primary_key=True)
    message = Column(LONGTEXT)
    offset = Column(Integer)
    timestamp = Column(DateTime)
    type = Column(String(50))
    idOffsetResponse = Column(Integer)
    tagMessage = Column(String(50))
    topic = Column(String(50))
    creator = Column(String(500))
    code = Column(String(20))
    partition = Column(Integer)


class Metric(Base):
    __tablename__ = 'Metric'

    Id = Column(Integer, primary_key=True)
    Field = Column(String(50))
    ValueUnit = Column(String(50))
    Type = Column(String(45))
    IsActive = Column(TINYINT(1))
    Parent = Column(String(50))
    Description = Column(String(200))


t_View_ConfigurationUser = Table(
    'View_ConfigurationUser', metadata,
    Column('Id', Integer, server_default=text("'0'")),
    Column('NameConfiguration', String(100)),
    Column('IdUser', Integer),
    Column('Symbol', CHAR(2)),
    Column('Value', Float),
    Column('IdFrequency', Integer),
    Column('Longitude', Float),
    Column('Latitude', Float),
    Column('DateTimeCreate', DateTime),
    Column('DateTimeUpdate', DateTime),
    Column('DateTimeActivation', DateTime),
    Column('IsActive', TINYINT(1)),
    Column('IdMetric', Integer),
    Column('FrequencyName', String(100)),
    Column('Minutes', Integer),
    Column('FrequencyIsActive', TINYINT(1)),
    Column('Field', String(50)),
    Column('Description', String(200)),
    Column('Parent', String(50)),
    Column('ValueUnit', String(50)),
    Column('Type', String(45)),
    Column('MetricIsActive', TINYINT(1))
)


class ConfigurationUser(Base):
    __tablename__ = 'ConfigurationUser'

    Id = Column(Integer, primary_key=True)
    NameConfiguration = Column(String(100))
    IdUser = Column(Integer)
    IdFrequency = Column(ForeignKey('Frequency.Id'), index=True)
    Longitude = Column(Float)
    Latitude = Column(Float)
    DateTimeCreate = Column(DateTime)
    DateTimeUpdate = Column(DateTime)
    DateTimeActivation = Column(DateTime)
    IsActive = Column(TINYINT(1))
    IdMetric = Column(ForeignKey('Metric.Id'), index=True)
    Symbol = Column(CHAR(2))
    Value = Column(Float)

    Frequency = relationship('Frequency')
    Metric = relationship('Metric')
