# coding: utf-8
from sqlalchemy import Column, DateTime, Float, Integer, String, Text
from sqlalchemy.dialects.mysql import LONGTEXT
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
metadata = Base.metadata

class HeartbeatSent(Base):
    __tablename__ = 'HeartbeatSent'

    id = Column(Integer, primary_key=True)
    datetime = Column(DateTime)
    partition = Column(Integer)
    cluster = Column(Integer)
    
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


class Notify(Base):
    __tablename__ = 'Notify'

    Id = Column(Integer, primary_key=True)
    IdUser = Column(Integer)
    IdSchedule = Column(Integer)
    Message = Column(Text)
    DateTimeCreate = Column(DateTime)
    IdConfiguration = Column(Integer)
    ValueWeather = Column(Float)
