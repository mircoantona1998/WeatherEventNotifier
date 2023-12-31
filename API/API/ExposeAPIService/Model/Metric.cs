﻿using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;

namespace ExposeAPI.Model
{
    public class Metric: IMetric
    {
        public int? Id { get; set; }
        public string? Field { get; set; }
        public string? Type { get; set; }
        public string? ValueUnit { get; set; }
        public string? Parent { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }

}
