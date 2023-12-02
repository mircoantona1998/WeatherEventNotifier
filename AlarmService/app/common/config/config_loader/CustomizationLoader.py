from common.config.config_paths import CUSTOMIZATION_PATH
from common.config.config_loader.BaseCustomizationLoader import BaseCustomizationLoader


class CustomizationLoader(BaseCustomizationLoader):

    def __init__(self, queue_name=""):
        super().__init__(CUSTOMIZATION_PATH, queue_name)


