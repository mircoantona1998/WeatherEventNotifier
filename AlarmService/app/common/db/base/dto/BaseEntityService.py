from sqlalchemy.orm.collections import InstrumentedList
from sqlalchemy import inspect
from datetime import datetime


class BaseEntityService:

    def __init__(self, entity):
        self.entity = entity
        self.dict_representation = {}

    def to_dict(self, get_only_keys=None, timestamps=False, add_params={}):
        self.dict_representation = {c.key: getattr(self.entity, c.key) for c in
                                    inspect(self.entity).mapper.column_attrs}
        self._filter_dict(get_only_keys=get_only_keys)
        self._adapt_timestamps(timestamps=timestamps)
        for key in add_params.keys():
            self.dict_representation[key] = add_params[key]
        return self.dict_representation

    def _adapt_timestamps(self, timestamps=False, factor=1000):
        if not timestamps:
            return
        for key in self.dict_representation.keys():
            if isinstance(self.dict_representation[key], datetime):
                self.dict_representation[key] = self.dict_representation[key].timestamp() * factor

    # def _to_full_dict(self, get_only_keys=None):
    #     self.dict_representation = {c.key: getattr(self.entity, c.key) for c in
    #                                 inspect(self.entity).mapper.column_attrs}
    #     self._filter_dict(get_only_keys=get_only_keys)
    #     return self.dict_representation

    def _filter_dict(self, get_only_keys=None):
        if get_only_keys is None:
            return
        else:
            key_list = [key for key in self.dict_representation.keys()]
            for key in key_list:
                if key not in get_only_keys:
                    try:
                        self.dict_representation.pop(key)
                    except KeyError:
                        pass

    # def loop_over_attrs(self, obj, dict_level: dict):
    #     for attr in dir(obj):
    #         item = getattr(obj, attr)
    #         if BaseEntityService._check_attr(item, attr):
    #             self.parse_attr(obj, attr, dict_level)

    # def parse_attr(self, obj, attr: str, dict_level):
    #     item = getattr(obj, attr)
    #     if item in self.items_covered:
    #         return
    #     if not isinstance(item, InstrumentedList):
    #         if "to_dict" in dir(item):
    #             if item not in self.items_covered:
    #                 dict_level[attr] = {}
    #                 self.items_covered.append(item)
    #                 self.loop_over_attrs(item, dict_level[attr])
    #         else:
    #             dict_level[attr] = item
# Codice vecchio
        # if isinstance(item, InstrumentedList):
        #      dict_level[attr] = []
        #      self.items_covered.append(item)
        #      for sub_item in item:
        #          dict_level[attr].append({})
        #          self.loop_over_attrs(sub_item, dict_level[attr][-1])
        # else:
        #     if "to_dict" in dir(item):
        #         if item not in self.items_covered:
        #             dict_level[attr] = {}
        #             self.items_covered.append(item)
        #             self.loop_over_attrs(item, dict_level[attr])
        #     else:
        #         dict_level[attr] = item

    @staticmethod
    def _check_attr(item, name):
        if not callable(item) and not name.startswith(
                "_") and not name == "metadata" and not name == "classes":
            return True
        else:
            return False

    @staticmethod
    def _check_attr_strong(item, name):
        if BaseEntityService._check_attr(item, name) and "to_dict" not in dir(
             item) and not isinstance(item, InstrumentedList):
            return True
        else:
            return False

    @staticmethod
    def _is_attr_field(item, name):
        return BaseEntityService._check_attr_strong(item, name)

    @staticmethod
    def _is_atrr_item(item, name):
        return BaseEntityService._check_attr(item, name) and "to_dict" in dir(item)

    @staticmethod
    def _is_attr_list(item, name):
        return BaseEntityService._check_attr(item, name) and isinstance(item, InstrumentedList)

