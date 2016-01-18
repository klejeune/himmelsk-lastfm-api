using System.Linq;
using System.Text;
using Himmelsk.LastFmApi.Engine;

namespace Himmelsk.LastFmApi.Services.LastFm.Credentials {
    public class MobileSessionCredentials : IAuthenticatedLastFmCredentials {
        private readonly string appId;
        private readonly string appSecret;
        private readonly string sessionKey;

        public MobileSessionCredentials(string endpoint, string appId, string appSecret, string sessionKey) {
            this.Endpoint = endpoint;
            this.appId = appId;
            this.appSecret = appSecret;
            this.sessionKey = sessionKey;
        }

        public void ApplyToRequest(ICommand command, System.Net.HttpWebRequest request) {

        }

        public string Endpoint { get; private set; }

        public static string md5(string text) {
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            MD5CryptoServiceProvider c = new MD5CryptoServiceProvider();
            buffer = c.ComputeHash(buffer);

            StringBuilder builder = new StringBuilder();
            foreach (byte b in buffer)
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }

        public void RegisterParameters(ICommand command, IParameterRegisterer parameters) {
            parameters.Register("sk", this.sessionKey, command.Verb);
            parameters.Register("api_key", this.appId, command.Verb);

            var paramsString =
                string.Join("", command.PostParameters.OrderBy(p => p.Key).Select(p => p.Key + p.Value))
                + this.appSecret;

            var sig = Md5.GetMd5String(paramsString);

            parameters.Register("api_sig", sig, command.Verb);

        }
    }
}