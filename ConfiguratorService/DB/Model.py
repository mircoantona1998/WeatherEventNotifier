# coding: utf-8
from sqlalchemy import Column, DateTime, Float, Integer, String
from sqlalchemy.dialects.mysql import BIT, LONGTEXT
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
metadata = Base.metadata


class EventsConfiguration(Base):
    __tablename__ = 'EventsConfiguration'

    Id = Column(Integer, primary_key=True)
    IdUser = Column(Integer)
    IdFrequencyType = Column(Integer)
    Longitude = Column(Float)
    Latitude = Column(Float)
    DateTimeCreate = Column(DateTime)
    DateTimeUpdate = Column(DateTime)
    DateActivation = Column(DateTime)
    IsActive = Column(BIT(1))
    IdMetric = Column(Integer)

    def as_dict(self):
        return {
            "Id": self.Id,
            "IdUser": self.IdUser,
            "IdFrequencyType": self.IdFrequencyType,
            "Longitude": float(self.Longitude) if self.Longitude is not None else None,
            "Latitude": float(self.Latitude) if self.Latitude is not None else None,
            "DateTimeCreate": self.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if self.DateTimeCreate is not None else None,
            "DateTimeUpdate": self.DateTimeUpdate.strftime('%Y-%m-%d %H:%M:%S') if self.DateTimeUpdate is not None else None,
            "DateActivation": self.DateActivation.strftime('%Y-%m-%d %H:%M:%S') if self.DateActivation is not None else None,
            "IsActive": bool(self.IsActive) if self.IsActive is not None else None,
            "IdMetric": self.IdMetric
        }

class FrequencyType(Base):
    __tablename__ = 'FrequencyType'

    Id = Column(Integer, primary_key=True)
    FrequencyName = Column(String(100))
    Minutes = Column(Integer)


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
