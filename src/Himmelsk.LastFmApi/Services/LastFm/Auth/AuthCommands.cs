using System.Threading.Tasks;
using Himmelsk.LastFmApi.Engine;
using Himmelsk.LastFmApi.Engine.Commands;
using Himmelsk.LastFmApi.Engine.Results;
using Himmelsk.LastFmApi.Services.LastFm.Auth.Results;
using Himmelsk.LastFmApi.Services.LastFm.Credentials;
using Himmelsk.Social.Api.Core.Common;

namespace Himmelsk.LastFmApi.Services.LastFm.Auth {
    class AuthCommands : IAuthCommands {
        private readonly ICommandExecuter<ILastFmCredentials> executer;
        private readonly string baseUrl = string.Empty;

        public AuthCommands(ICommandExecuter<ILastFmCredentials> executer) {
            this.executer = executer;
        }

        public async Task<IResult<Lfm>> GetToken(ILastFmCredentials credentials)
        {
            var command = new LastFmCommand<Lfm>(this.baseUrl, false)
                .RegisterParameter(r => r.RegisterGet("method", "auth.getToken"));

            return await this.executer.ExecuteAsync(credentials, command);
        }

        public async Task<IResult<AuthGetMobileSessionResult>> GetMobileSession(ILastFmCredentials credentials, string username, string password, string apiKey) {
           var command = new LastFmCommand<AuthGetMobileSessionResult>(this.baseUrl, false)
                .RegisterParameter(r => r.RegisterGet("method", "auth.getMobileSession"))
                .RegisterParameter(r => r.RegisterGet("username", username))
                .RegisterParameter(r => r.RegisterGet("password", password))
                .RegisterParameter(r => r.RegisterGet("api_key", apiKey));

            return await this.executer.ExecuteAsync(credentials, command);
        }
    }
}