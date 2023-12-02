from logging.handlers import RotatingFileHandler, TimedRotatingFileHandler
import logging
from sqlalchemy.ext.automap import automap_base
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from sqlalchemy.pool import NullPool
from common.config.config_loader.CustomizationLoader import CustomizationLoader
from common.db.base.entities.BaseEntity import BaseEntity
from datetime import datetime

cfg_loader = CustomizationLoader()

now = datetime.now()
try:
    Base = automap_base(cls=BaseEntity)
    Engine = create_engine(cfg_loader.load_db(), pool_pre_ping=True)  # , poolclass=NullPool)
    Base.prepare(Engine, reflect=True)
    Session = sessionmaker(bind=Engine)
except Exception as e:
    print(e)
    Base = automap_base(cls=BaseEntity)
    Engine = create_engine(cfg_loader.load_backup_db(), pool_pre_ping=True)  # , poolclass=NullPool)
    Base.prepare(Engine, reflect=True)
    Session = sessionmaker(bind=Engine)
    print("USING BACKUP DATABASE")

print("Engine for background module built in {} s".format(datetime.now().timestamp() - now.timestamp()))

# create logger su console
logger = logging.getLogger(__name__)
logHandler = logging.StreamHandler()
# formatter = jsonlogger.JsonFormatter(timestamp=True)
# logHandler.setFormatter(formatter)
logger.addHandler(logHandler)
# create logger su file
path = cfg_loader.get_loger_file_path()
logger.setLevel(logging.DEBUG)




