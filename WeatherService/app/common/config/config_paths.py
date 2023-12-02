import os.path

CUSTOMIZATION_PATH = os.path.realpath(os.path.join(os.path.dirname(os.path.realpath(__file__)), "../../customization"))

print(CUSTOMIZATION_PATH)
