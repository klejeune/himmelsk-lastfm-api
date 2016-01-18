using System.Threading.Tasks;
using Himmelsk.LastFmApi.Engine;
using Himmelsk.LastFmApi.Engine.Commands;
using Himmelsk.LastFmApi.Engine.Results;
using Himmelsk.LastFmApi.Services.LastFm.Credentials;
using Himmelsk.LastFmApi.Services.LastFm.User.Results;
using Himmelsk.Social.Api.Core.Common;
using Himmelsk.Social.Api.Core.LinkedIn.Results;

namespace Himmelsk.LastFmApi.Services.LastFm.User
{
    class UserCommands : IUserCommands
    {
        private ICommandExecuter<ILastFmCredentials> executer;
        private readonly string baseUrl = string.Empty;

        public UserCommands(ICommandExecuter<ILastFmCredentials> executer)
        {
            this.executer = executer;
        }

        public async Task<IResult<GetRecentTracksResult>> GetRecentTracks(ILastFmCredentials credentials, string user, int? page, int? from, int? to,
            int? limit, bool? extended = null)
        {
            var command = new LastFmCommand<GetRecentTracksResult>(this.baseUrl, false)
                .RegisterParameter(r => r.RegisterPost("method", "user.getRecentTracks"))
                .RegisterParameter(r => r.RegisterPost("user", user))
                .RegisterParameter(r => r.RegisterPost("page", page))
                .RegisterParameter(r => r.RegisterPost("from", from))
                .RegisterParameter(r => r.RegisterPost("to", to))
                .RegisterParameter(r => r.RegisterPost("limit", limit))
                .RegisterParameter(r => r.RegisterPost("extended", extended));

            return await this.executer.ExecuteAsync(credentials, command);
        }
    }
}