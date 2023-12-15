class KafkaMessage:
    def __init__(self, IdOffsetResponse, Type, Tag, Data):
        self.IdOffsetResponse = IdOffsetResponse
        self.Type = Type
        self.Tag = Tag
        self.Data = Data
