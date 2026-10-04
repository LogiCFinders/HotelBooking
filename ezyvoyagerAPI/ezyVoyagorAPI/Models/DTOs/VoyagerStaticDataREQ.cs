using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyvoyagerAPI.Models.DTOs
{
    public class VoyagerStaticDataREQ
    {
        public string Action { get; set; }
        public string RequestFor { get; set; }
        public Int64 CompanyId { get; set; }
        public List<VoyagerStaticData> Data { get; set; }
    }

    public class VoyagerStaticDataRESP
    {
        public string Action { get; set; }
        public string RequestFor { get; set; }
        public Int64 CompanyId { get; set; }
        public List<VoyagerStaticData> Data { get; set; }
        public string Message { get; set; }
        public bool isSuccess { get; set; }
    }

    public class VoyagerStaticData
    {
        public Int64 ID { get; set; }
        public string value { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}