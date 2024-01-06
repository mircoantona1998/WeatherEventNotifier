import logging

class Logger:
    def __init__(self, log_file_path='app.log'):
        logging.basicConfig(filename=log_file_path, level=logging.INFO)
        self.logger = logging.getLogger(__name__)

    def log_action(self, action_description):
        self.logger.info(action_description)

