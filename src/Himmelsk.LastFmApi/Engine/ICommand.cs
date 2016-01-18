using System;
using System.Collections.Generic;
using Himmelsk.Social.Api.Core.Common;
using Himmelsk.Social.Api.Core.LinkedIn.Results;

namespace Himmelsk.LastFmApi.Engine {
    public interface ICommand<out TResult> : ICommand {
        TResult FormatResult(string value, string contentType);
        void LoadParameters(ICredentials credentials);
    }

    public interface ICommand {
        IEnumerable<IParameter> PostParameters { get; }
        IEnumerable<IParameter> GetParameters { get; }
        IEnumerable<IParameter> Parameters { get; }
        string PostContent { get; }
        string BaseUrl { get; }
        CommandVerb Verb { get; }
        string ContentType { get; }
        void AddHeaders(Action<string, string> add);
        Error FormatError(string value, string contentType);
    }
}
