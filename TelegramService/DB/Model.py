# coding: utf-8
from sqlalchemy import CHAR, Column, DateTime, Integer, String
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


class TelegramConfiguration(Base):
    __tablename__ = 'TelegramConfiguration'

    Id = Column(Integer, primary_key=True)
    bot = Column(String(50))
    token = Column(String(100))


class TelegramMessage(Base):
    __tablename__ = 'TelegramMessages'

    Id = Column(Integer, primary_key=True)
    IdChat = Column(LONGTEXT)
    Testo = Column(LONGTEXT)
    Allegati = Column(TINYINT(1))
    DateCreate = Column(DateTime)
    DateUpdate = Column(DateTime)
    WasSent = Column(TINYINT(1))
    DateSent = Column(DateTime)
    isActive = Column(TINYINT(1), nullable=False)
    Result = Column(LONGTEXT)


class TelegramUser(Base):
    __tablename__ = 'TelegramUsers'

    id = Column(Integer, primary_key=True)
    idUser = Column(Integer, nullable=False)
    chat_id = Column(CHAR(10), nullable=False)
