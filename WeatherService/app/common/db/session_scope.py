from contextlib import contextmanager
from common.db import Session, logger
import datetime


@contextmanager
def session_scope(name=None):
    """Provide a transactional scope around a series of operations."""
    if name is None:
        name = "MODULE NOT SPECIFIED (add session_scope name parameter)."
    now = datetime.datetime.now().timestamp()
    session = Session()
    try:
        try:
            logger.debug("{}: {} opens connection id {}".format(__name__, name, session.connection().connection.thread_id()))
        except:
            logger.debug("{}: {} opens connection.".format(__name__, name))
        yield session
        session.commit()
    except Exception as e:
        try:
            logger.debug("{}: {} releasing connection id {} after Exception. Session opened for {} s".format(__name__, name, session.connection().connection.thread_id(), datetime.datetime.now().timestamp() - now))
        except:
            logger.debug(
                "{}: {} releasing connection after Exception. Session opened for {} s".format(__name__, name,
                                                                                              datetime.datetime.now().timestamp() - now))
        session.rollback()
        raise e
    finally:
        try:
            logger.debug("{}: {} releasing connection id {}. Session activity: {} s".format(__name__, name, session.connection().connection.thread_id(), datetime.datetime.now().timestamp() - now))
        except:
            logger.debug("{}: {} releasing connection. Session activity: {} s".format(__name__, name,
                                                                                      datetime.datetime.now().timestamp() - now))
        session.connection().invalidate()
        session.close()

