using System.Threading.Tasks;
using Himmelsk.LastFmApi.Engine;
using Himmelsk.LastFmApi.Services.LastFm;
using Himmelsk.LastFmApi.Services.LastFm.Album;
using Himmelsk.LastFmApi.Services.LastFm.Auth;
using Himmelsk.LastFmApi.Services.LastFm.Credentials;
using Himmelsk.LastFmApi.Services.LastFm.User;

namespace Himmelsk.LastFmApi
{
    public class LastFm
    {
        private readonly string endpoint = "http://ws.audioscrobbler.com/2.0/";

        public LastFm()
        {
            var executer = new CommandExecuter<ILastFmCredentials>();

            this.User = new UserCommands(executer);
            this.Auth = new AuthCommands(executer);
        }

        public IUserCommands User { get; private set; }
        public IAuthCommands Auth { get; private set; }

        public async Task<ILastFmCredentials> GetAppCredentials(string appId, string appSecret)
        {
            var cred = new AppCredentials(this.endpoint, appId, appSecret);

            var token = await this.Auth.GetToken(cred);

            return cred;

            //var newCred = new DesktopTokenCredentials(this.endpoint, appId, appSecret, token.Typed.Token);

            //return newCred;
        }

        public async Task<IAuthenticatedLastFmCredentials> GetMobileSessionCredentials(string username, string password, string appId,
            string appSecret)
        {
            var cred = new AppCredentials(this.endpoint, appId, appSecret);

            var sessionKey = await this.Auth.GetMobileSession(cred, username, password, appId);

            var newCred = new MobileSessionCredentials(this.endpoint, appId, appSecret, sessionKey.Typed.Session.Key);

            return newCred;
        }
    }
}
