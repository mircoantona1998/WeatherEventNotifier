from common.db.base.dto.BaseEntityService import BaseEntityService


class BaseEntity:

    def to_dict(self, get_only_keys=None, timestamps=False, add_params={}):
        dto_service = BaseEntityService(self)
        return dto_service.to_dict(get_only_keys=get_only_keys, timestamps=timestamps, add_params=add_params)

    def to_easy_dict(self, get_only_keys=None):
        dto_service = BaseEntityService(self)
        return dto_service.to_dict(get_only_keys=get_only_keys)

    def safe_get(self, attribute_name: str, default=None):
        try:
            return getattr(self, attribute_name)
        except AttributeError:
            return default

    @classmethod
    def get_column(cls, column_name: str, default=None):
        try:
            return getattr(cls, column_name)
        except AttributeError:
            return default

    def __getitem__(self, key):
        try:
            return
        except AttributeError:
            return None

    def __setitem__(self, key, value):
        setattr(self, key, value)
