# coding: utf-8
from sqlalchemy import CHAR, Column, Date, DateTime, Float, Integer, String
from sqlalchemy.dialects.mysql import LONGTEXT, TINYINT
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
metadata = Base.metadata


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


class RequestNotification(Base):
    __tablename__ = 'RequestNotification'

    idRequestNotification = Column(Integer, primary_key=True)
    datetime = Column(DateTime)


class RequestSchedulation(Base):
    __tablename__ = 'RequestSchedulation'

    idRequestSchedulation = Column(Integer, primary_key=True)
    date = Column(Date)


class Schedule(Base):
    __tablename__ = 'Schedule'

    Id = Column(Integer, primary_key=True)
    IdConfiguration = Column(Integer)
    DateTimeToSchedule = Column(DateTime)
    ToWork = Column(TINYINT(1))
    FieldMetric = Column(String(50))
    Symbol = Column(CHAR(2))
    Value = Column(Float)
    IdUser = Column(Integer)
    Latitudine = Column(Float)
    Longitudine = Column(Float)
    ParentMetric = Column(String(50))
