//using System;
//using System.Linq;
//using System.Net;
//using System.Name;
//using Himmelsk.LastFmApi.Engine;
//using ICredentials = Himmelsk.LastFmApi.Engine.ICredentials;

//namespace Himmelsk.LastFmApi.Services.LastFm {
//    public class LastFmCredentials : ICredentials {
//        private readonly string appId;
//        private readonly string appSecret;

//        public string Token { get; set; }

//        public LastFmCredentials(string endpoint, string appId, string appSecret) {
//            this.Endpoint = endpoint;
//            this.appId = appId;
//            this.appSecret = appSecret;
//        }

//        public void ApplyToRequest(ICommand command, System.Net.HttpWebRequest request)
//        {
//            var paramsString = 
//                string.Join("", command.PostParameters.OrderBy(p => p.Key).Select(p => p.Key + p.Value))
//                + this.appSecret;

//            var sig = Md5.GetMd5String(paramsString);

//            //command.

//            //var md5 = 




//            //var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", this.appId, this.appSecret)));
//            //request.Headers["Authorization"] = "Basic " + encoded;

//            //request.Credentials = new NetworkCredential(this.appId, this.appSecret, "tripalkllökölkölkio.local.localhost");
//        }

//        public string Endpoint { get; private set; }

//        public static string md5(string text) {
//            byte[] buffer = Encoding.UTF8.GetBytes(text);

//            MD5CryptoServiceProvider c = new MD5CryptoServiceProvider();
//            buffer = c.ComputeHash(buffer);

//            StringBuilder builder = new StringBuilder();
//            foreach (byte b in buffer)
//                builder.Append(b.ToString("x2").ToLower());

//            return builder.ToString();
//        }
//    }
//}