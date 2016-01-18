using System.Threading.Tasks;
using Himmelsk.LastFmApi.Engine.Results;
using Himmelsk.Social.Api.Core.Common;
using Himmelsk.Social.Api.Core.LinkedIn.Results;

namespace Himmelsk.LastFmApi.Engine {
    interface ICommandExecuter<in TCredentials> where TCredentials : ICredentials {
        Task<IResult<TResult>> ExecuteAsync<TResult>(TCredentials credentials, ICommand<TResult> command);
        Task<IResult<TResult>> Execute<TResult>(TCredentials credentials, ICommand<TResult> command);
    }
}
