

namespace SLAManagerdata.ViewModels
{
    #region Sla
    public class SlaAddDTO
    {
        public string NameSla { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int IdMetric { get; set; }
        public int IdFrequency { get; set; }
        public string Symbol { get; set; }
        public float Value { get; set; }
        public DateTime? DateTimeActivation{ get; set; }
    }
    public class SlaCreateRequestKafka : SlaAddDTO
    {
        public int IdUser { get; set; }
        public static SlaCreateRequestKafka ConvertSlaCreateToRequestKafka(SlaAddDTO dto, int idUser)
        {
            return new SlaCreateRequestKafka
            {
                NameSla = dto.NameSla,   
                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                IdMetric = dto.IdMetric,
                IdFrequency = dto.IdFrequency,
                DateTimeActivation = dto.DateTimeActivation,
                Symbol=dto.Symbol,
                Value=dto.Value,
                IdUser = idUser
            };
        }
    }
    
    public class SlaPatchDTO
    {
        public int IdSla { get; set; }
        public string? NameSla { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public int? IdMetric { get; set; }
        public int? IdFrequency { get; set; }
        public string? Symbol { get; set; }
        public float? Value { get; set; }
        public DateTime? DateTimeActivation { get; set; }
        public bool? IsActive { get; set; }
    }
    public class SlaPatchRequestKafka : SlaPatchDTO
    {
        public int IdUser { get; set; }
        public static SlaPatchRequestKafka ConvertSlaPatchToRequestKafka(SlaPatchDTO dto, int idUser)
        {
            return new SlaPatchRequestKafka
            {
                NameSla = dto.NameSla,  
                IdSla = dto.IdSla,
                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                IdMetric = dto.IdMetric,
                IdFrequency = dto.IdFrequency,
                DateTimeActivation = dto.DateTimeActivation,
                Symbol = dto.Symbol,
                Value = dto.Value,
                IdUser = idUser,
                IsActive= dto.IsActive
            };
        }
    }
    
    #endregion
}