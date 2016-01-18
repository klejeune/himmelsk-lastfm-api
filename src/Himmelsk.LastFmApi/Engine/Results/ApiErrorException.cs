using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Himmelsk.LastFmApi.Engine.Results;

namespace Himmelsk.Social.Api.Core.LinkedIn.Results {
    public class ApiErrorException : Exception {
        public ApiErrorException(IResult<Error> result) : base("Error on API call:\n" + result.Raw) {
            this.Result = result;
        }

        public IResult<Error> Result { get; private set; }
    }
}
