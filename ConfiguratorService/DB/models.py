# coding: utf-8
from sqlalchemy import Column, DateTime, Float, Integer, String
from sqlalchemy.dialects.mysql import BIT, LONGTEXT, TINYINT
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
metadata = Base.metadata


class EventsConfiguration(Base):
    __tablename__ = 'EventsConfiguration'

    Id = Column(Integer, primary_key=True)
    IdUser = Column(Integer)
    IdFrequency = Column(Integer)
    Longitude = Column(Float)
    Latitude = Column(Float)
    DateTimeCreate = Column(DateTime)
    DateTimeUpdate = Column(DateTime)
    DateActivation = Column(DateTime)
    IsActive = Column(TINYINT(1))
    IdMetric = Column(Integer)


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


class Metric(Base):
    __tablename__ = 'Metric'

    Id = Column(Integer, primary_key=True)
    Field = Column(String(50))
    ValueUnit = Column(String(50))
    Type = Column(String(45))
    IsActive = Column(BIT(1))
