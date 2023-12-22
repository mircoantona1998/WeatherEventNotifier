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


class Schedule(Base):
    __tablename__ = 'Schedule'

    Id = Column(Integer, primary_key=True)
    IdConfiguration = Column(Integer)
    DateTimeToSchedule = Column(DateTime)
    ToWork = Column(TINYINT(1))
    def as_dict(self):
        return {
            "Id": self.Id,
            "IdConfiguration":self.IdConfiguration,
            "DateTimeToSchedule": self.DateTimeToSchedule.strftime('%Y-%m-%d %H:%M:%S') if self.DateTimeToSchedule is not None else None,
            "ToWork": bool(self.ToWork) if self.ToWork is not None else None,
        }


class Notify(Base):
    __tablename__ = 'Notify'

    Id = Column(Integer, primary_key=True)
    IdUser = Column(Integer)
    IdSchedule = Column(ForeignKey('Schedule.Id', ondelete='CASCADE'), index=True)
    Message = Column(Text)
    DateTimeCreate = Column(DateTime)
    IsActive = Column(TINYINT(1))
    ToWork = Column(TINYINT(1))

    Schedule = relationship('Schedule')
    
    def as_dict(self):
        return {
            "Id": self.Id,
            "IdUser": self.IdUser,
            "IdSchedule": self.IdSchedule,
            "Message":self.Message,
            "DateTimeCreate": self.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if self.DateTimeCreate is not None else None,
            "IsActive": bool(self.IsActive) if self.IsActive is not None else None,
            "ToWork": bool(self.ToWork) if self.ToWork is not None else None,
        }


