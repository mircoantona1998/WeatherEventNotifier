# coding: utf-8
from sqlalchemy import CHAR, Column, DateTime, Integer, String
from sqlalchemy.dialects.mysql import LONGTEXT, TINYINT
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
metadata = Base.metadata


class HeartbeatSent(Base):
    __tablename__ = 'HeartbeatSent'

    id = Column(Integer, primary_key=True)
    datetime = Column(DateTime)
    partition = Column(Integer)
    cluster = Column(Integer)
class Mail(Base):
    __tablename__ = 'Mail'

    Id = Column(Integer, primary_key=True)
    IdUser = Column(Integer, nullable=False)
    Mittente = Column(String(100))
    Destinatario = Column(LONGTEXT)
    Oggetto = Column(LONGTEXT)
    Testo = Column(LONGTEXT)
    Allegati = Column(TINYINT(1))
    DateCreate = Column(DateTime)
    WasSent = Column(TINYINT(1))
    Result = Column(LONGTEXT)


class MailConfiguration(Base):
    __tablename__ = 'MailConfiguration'

    Id = Column(Integer, primary_key=True)
    mail = Column(String(50))
    name = Column(String(50))
    password = Column(String(100))


class MailUsers(Base):
    __tablename__ = 'MailUsers'

    id = Column(Integer, primary_key=True)
    idUser = Column(Integer, nullable=False)
    mail = Column(String(500), nullable=False)
    isActive = Column(TINYINT(1), nullable=False)
    


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
