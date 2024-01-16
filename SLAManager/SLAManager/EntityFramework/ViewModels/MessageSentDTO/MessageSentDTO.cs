﻿

namespace SLAManagerdata.ViewModels
{
    #region MESSAGESENT
    public partial class MessageSentDTO
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int? Offset { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Type { get; set; }
        public int? IdOffsetResponse { get; set; }
        public string? TagMessage { get; set; }
        public string? Topic { get; set; }
        public string? Creator { get; set; }
        public string? Code { get; set; }
        public int? Partition { get; set; }
    }
   
    #endregion
}