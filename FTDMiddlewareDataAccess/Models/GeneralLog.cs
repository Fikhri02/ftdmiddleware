using System;
using FTDMiddlewareDataAccess.Models.RequestModels;
using FTDMiddlewareDataAccess.Models.ResponseModels;

namespace FTDMiddlewareDataAccess.Models
{
    public class GeneralLog
    {
        public string RequestPath { get; set; }
        public RequestModels.Base RequestBody { get; set; }  
        public ResponseModels.Base ResponseBody { get; set; } 
        public DateTime Time { get; set; }
    }
}
