using System.Linq;
using Himmelsk.LastFmApi.Engine;

namespace Himmelsk.LastFmApi.Services.LastFm.Credentials {
    public class AppCredentials : ILastFmCredentials {
        private readonly string appId;
        private readonly string appSecret;
        private readonly string endpoint;

        public AppCredentials(string endpoint, string appId, string appSecret) {
            this.endpoint = endpoint;
            this.appId = appId;
            this.appSecret = appSecret;
        }

        public void ApplyToRequest(ICommand command, System.Net.HttpWebRequest request) {

        }

        public string Endpoint {
            get { return this.endpoint; }
        }

        public void RegisterParameters(ICommand command, IParameterRegisterer parameters) {
            parameters.Register("api_key", this.appId, command.Verb);

            //var paramsString =
            //  string.Join("", command.Parameters.OrderBy(p => p.Key).Select(p => p.Key + p.Value))
            //  + this.appSecret;

            //var sig = Md5.GetMd5String(paramsString);

            //parameters.Register("api_sig", sig, command.Verb);
        }
    }
}