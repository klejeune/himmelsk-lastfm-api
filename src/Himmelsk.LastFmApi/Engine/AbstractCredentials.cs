//using System;
//using System.Net;
//using System.Name;

//namespace Himmelsk.LastFmApi.Engine {
//    class AbstractCredentials : ICredentials {
//         private readonly string appId;
//        private readonly string appSecret;

//        public AbstractCredentials(string endpoint, string appId, string appSecret)
//        {
//            this.Endpoint = endpoint;
//            this.appId = appId;
//            this.appSecret = appSecret;
//        }

//        public void ApplyToRequest(LastFmApi.Engine.ICommand command, System.Net.HttpWebRequest request) {
//            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", this.appId, this.appSecret)));
//            request.Headers["Authorization"] = "Basic " + encoded;
            
//            request.Credentials = new NetworkCredential(this.appId, this.appSecret, "tripalio.local.localhost");
//        }

//        public string Endpoint { get; private set; }
        
//        public virtual void RegisterParameters(LastFmApi.Engine.ICommand command, LastFmApi.Engine.IParameterRegisterer parameters) {
      
//        }
//    }
//}
