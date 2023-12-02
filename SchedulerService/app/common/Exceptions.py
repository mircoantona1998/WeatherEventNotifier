class ReceiverException(Exception):

    def __init__(self, message=None, *args):
        self.message = message
        super().__init__(*args)

    def __str__(self):
        return "Receiver exception." if self.message is None else self.message


class DaoException(Exception):

    def __init__(self, message=None, *args):
        self.message = message
        super().__init__(*args)

    def __str__(self):
        return "Dao exception." if self.message is None else self.message


class JobException(Exception):

    def __init__(self, message=None, *args):
        self.message = message
        super().__init__(*args)

    def __str__(self):
        return "Job exception." if self.message is None else self.message


class DalException(Exception):

    def __init__(self, message=None, *args):
        self.message = message
        super().__init__(*args)

    def __str__(self):
        return "DalException. " + self.message if self.message is not None else ""


class ServiceException(Exception):

    def __init__(self, message=None, *args):
        self.message = message
        self.message_prefix = "ServiceException. "
        super().__init__(*args)

    def __str__(self):
        return self.message_prefix + self.message if self.message is not None else ""


class DataException(Exception):

    def __init__(self, message=None, *args):
        self.message = message if message is not None else ""
        super().__init__(*args)

    def __str__(self):
        return "Data exception. " + self.message


class DbConfigurationException(Exception):

    def __init__(self, message=None, *args):
        self.message = message
        super().__init__(*args)

    def __str__(self):
        return "DbConfigurationException Exception." if self.message is None else self.message


class ApiException(Exception):

    def __init__(self, message=None, *args):
        self.message = message
        super().__init__(*args)

    def __str__(self):
        return "ApiException. " + self.message if self.message is not None else ""


class InputException(Exception):

    def __init__(self, message=None, *args):
        self.message = message
        super().__init__(*args)

    def __str__(self):
        return "InputException. " + self.message if self.message is not None else ""


class DataBaseException(Exception):

    def __init__(self, message=None, *args):
        if message is not None:
            self.message = message
        else:
            self.message = "Error to connect with db."
        super().__init__(*args)

    def __str__(self):
        return self.message


class MicroservicesResponseException(Exception):

    def __init__(self, error_ref=None, *args):
        self.error_ref = error_ref
        self.message = "Microservice response fail."
        super().__init__(*args)

    def __str__(self):
        return "MicroserviceResponseException error_ref:" + str(self.error_ref)


