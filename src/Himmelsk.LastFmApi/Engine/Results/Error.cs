using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himmelsk.Social.Api.Core.LinkedIn.Results {
    public class Error {
        int ErrorCode { get; set; }
        string Message { get; set; }
        string RequestId { get; set; }
        int Status { get; set; }
        int Timestamp { get; set; }
    }
}
