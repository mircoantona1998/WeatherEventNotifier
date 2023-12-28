# coding: utf-8
from sqlalchemy import Column, DateTime, ForeignKey, Integer, String, Text
from sqlalchemy.dialects.mysql import LONGTEXT, TINYINT
from sqlalchemy.orm import relationship
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


class Schedule(Base):
    __tablename__ = 'Schedule'

    Id = Column(Integer, primary_key=True)
    IdConfiguration = Column(Integer)
    DateTimeToSchedule = Column(DateTime)
    ToWork = Column(TINYINT(1))


class Notify(Base):
    __tablename__ = 'Notify'

    Id = Column(Integer, primary_key=True)
    IdUser = Column(Integer)
    IdSchedule = Column(ForeignKey('Schedule.Id', ondelete='CASCADE', onupdate='RESTRICT'), index=True)
    Message = Column(Text)
    DateTimeCreate = Column(DateTime)
    IsActive = Column(TINYINT(1))
    ToWork = Column(TINYINT(1))

    Schedule = relationship('Schedule')
