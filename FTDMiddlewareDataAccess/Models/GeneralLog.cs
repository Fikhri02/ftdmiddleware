using System;
using FTDMiddlewareDataAccess.Models.RequestModels;
using FTDMiddlewareDataAccess.Models.ResponseModels;

namespace FTDMiddlewareDataAccess.Models
{
    public class GeneralLog
    {
        public string RequestPath { get; set; }
        public RequestModels.Base RequestBody { get; set; }  // Just use 'Base' directly
        public ResponseModels.Base ResponseBody { get; set; } // Just use 'Base' directly
        public DateTime Time { get; set; }
    }
}
