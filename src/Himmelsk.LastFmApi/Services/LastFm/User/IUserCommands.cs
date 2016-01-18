using System.Threading.Tasks;
using Himmelsk.LastFmApi.Engine;
using Himmelsk.LastFmApi.Engine.Results;
using Himmelsk.LastFmApi.Services.LastFm.Credentials;
using Himmelsk.LastFmApi.Services.LastFm.User.Results;

namespace Himmelsk.LastFmApi.Services.LastFm.User
{
    public interface IUserCommands
    {
        Task<IResult<GetRecentTracksResult>> GetRecentTracks(ILastFmCredentials credentials, string user, int? page = null, int? from = null, int? to = null,
            int? limit = null, bool? extended = null);
    }
}