using System;
using System.Collections.Generic;
using Himmelsk.Social.Api.Core.Common;

namespace Himmelsk.LastFmApi.Engine {
    public interface IParameterRegisterer {
        void RegisterGet(string name, string value);
        void RegisterGet(string name, bool value);
        void RegisterGet(string name, bool? value);
        void RegisterGet(string name, int? value);
        void RegisterGet(string name, DateTime? value, string format);
        void RegisterPost(string name, string value);
        void RegisterPost(string name, bool value);
        void RegisterPost(string name, bool? value);
        void RegisterPost(string name, int? value);
        void RegisterPost(string name, DateTime? value, string format);
        void RegisterPostContent(dynamic value);

        void Register(string name, string value, CommandVerb verb);
        void Register(string name, bool value, CommandVerb verb);
        void Register(string name, bool? value, CommandVerb verb);
        void Register(string name, int? value, CommandVerb verb);
        void Register(string name, DateTime? value, string format, CommandVerb verb);

        IDictionary<string, string> ParametersPost { get; }
        IDictionary<string, string> ParametersGet { get; }
        IDictionary<string, string> Parameters { get; }
        string PostContent { get; }
    }
}
