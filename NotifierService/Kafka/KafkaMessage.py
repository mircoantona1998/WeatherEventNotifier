class KafkaMessage:
    def __init__(self, IdOffsetResponse, Type, Tag, Data,Code=True):
        self.IdOffsetResponse = IdOffsetResponse
        self.Type = Type
        self.Tag = Tag
        self.Code = Code
        self.Data = Data
