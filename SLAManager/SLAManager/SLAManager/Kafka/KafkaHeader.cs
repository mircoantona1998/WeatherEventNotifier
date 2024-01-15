using Confluent.Kafka;
using System.Reflection.PortableExecutable;
using System.Text;

namespace SLAManager.Kafka
{
    public class KafkaHeader
    {
        public string? Creator { get; set; }
        public int IdOffsetResponse { get; set; }
        public string? Type { get; set; }
        public string? Tag { get; set; }
        public string? Code { get; set; }

        public KafkaHeader(Headers? headers,int partition) {
            var senderIdHeader = headers.GetLastBytes("Creator");
            if (senderIdHeader != null)
            {
                this.Creator = Encoding.UTF8.GetString(senderIdHeader);
            }

            var IdoffsetResponseB = headers.GetLastBytes("IdOffsetResponse");
            if (IdoffsetResponseB != null)
            {
                string IdoffsetResponseString = Encoding.UTF8.GetString(IdoffsetResponseB);
                this.IdOffsetResponse = int.Parse(IdoffsetResponseString);
            }
            var TypeB = headers.GetLastBytes("Type");
            if (TypeB != null)
            {
               this.Type = Encoding.UTF8.GetString(TypeB);
            }
            var TagB = headers.GetLastBytes("Tag");
            if (TagB != null)
            {
                this.Tag = Encoding.UTF8.GetString(TagB);
            }
            var CodB = headers.GetLastBytes("Code");
            if (CodB != null)
            {
                this.Code = Encoding.UTF8.GetString(CodB);
            }
        }
        public override string ToString()
        {
            return $"Creator: {Creator}, IdOffsetResponse: {IdOffsetResponse}, Type: {Type}, Tag: {Tag}, Code: {Code}";
        }
    }

}
