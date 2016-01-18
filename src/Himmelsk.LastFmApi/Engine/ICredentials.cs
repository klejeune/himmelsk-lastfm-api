using System.Net;
using Himmelsk.Social.Api.Core.Common;

namespace Himmelsk.LastFmApi.Engine {
    public interface ICredentials {
        void ApplyToRequest(ICommand command, HttpWebRequest request);
        string Endpoint { get; }
        void RegisterParameters(ICommand command, IParameterRegisterer parameters);
    }
}
