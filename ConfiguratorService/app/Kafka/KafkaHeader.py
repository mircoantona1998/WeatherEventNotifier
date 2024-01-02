from Utils.EnumMessageCode import MessageCode
class KafkaHeader:
    def __init__(self, IdOffsetResponse=None, Type=None, Tag=None, Code=MessageCode.Ok.value,Creator=None, header=None):
        if header is not None:
            self.IdOffsetResponse = header["IdOffsetResponse"]
            self.Type = header["Type"]
            self.Tag = header["Tag"]
            self.Code = header["Code"]
            self.Creator = header["Creator"]
        else:
            self.headers_list = [
                ("IdOffsetResponse", str(IdOffsetResponse) ),
                ("Type",  str(Type)),
                ("Tag",  str(Tag)),
                ("Code",  str(Code)),
                ("Creator",  str(Creator)),
            ]