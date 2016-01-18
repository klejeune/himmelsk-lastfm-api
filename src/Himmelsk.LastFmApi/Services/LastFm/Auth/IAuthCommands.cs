using System.Threading.Tasks;
using Himmelsk.LastFmApi.Engine;
using Himmelsk.LastFmApi.Engine.Results;
using Himmelsk.LastFmApi.Services.LastFm.Auth.Results;
using Himmelsk.LastFmApi.Services.LastFm.Credentials;

namespace Himmelsk.LastFmApi.Services.LastFm.Auth
{
    public interface IAuthCommands
    {
        Task<IResult<Lfm>> GetToken(ILastFmCredentials credentials);
        Task<IResult<AuthGetMobileSessionResult>> GetMobileSession(ILastFmCredentials credentials, string username, string password, string apiKey);
    }
}