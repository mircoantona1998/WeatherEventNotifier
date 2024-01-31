# models.py

from sqlalchemy import Column, DATETIME, BOOLEAN, INTEGER, VARCHAR, NVARCHAR, FLOAT
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()


class Users(Base):
    __tablename__ = "Users"
    Id = Column(INTEGER, primary_key=True)
    Nome = Column(VARCHAR(50), nullable=True)
    Cognome = Column(VARCHAR(50), nullable=True)
    Username = Column(VARCHAR(50), nullable=True)
    Password = Column(VARCHAR(50), nullable=True)
    isActive = Column(BOOLEAN, nullable=True)
    DateUpdate = Column(DATETIME, nullable=True)
    Address = Column(VARCHAR(50), nullable=True)
    Cap = Column(VARCHAR(50), nullable=True)
    City = Column(VARCHAR(50), nullable=True)
    LastAccess = Column(DATETIME, nullable=True)
    isBlocked = Column(INTEGER, nullable=True)
    Partition = Column(INTEGER, nullable=True)
    Cluster = Column(INTEGER, nullable=True)
