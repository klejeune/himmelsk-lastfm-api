using System;
using System.Collections.Generic;
using Himmelsk.Social.Api.Core.Common;

namespace Himmelsk.LastFmApi.Engine.Commands
{
    class GenericCommand<TResult> : AbstractCommand<TResult> where TResult : class
    {
        public GenericCommand(string baseUrl, CommandVerb verb)
        {
            this.verb = verb;
            this.url = url;
        }

        private CommandVerb verb;
        private string url;
        private List<Action<IParameterRegisterer>> registrations = new List<Action<IParameterRegisterer>>();

        public override CommandVerb Verb {
            get { return verb; }
        }

        public GenericCommand<TResult> RegisterParameter(Action<IParameterRegisterer> regitration)
        {
            this.registrations.Add(regitration);
            return this;
        }

        protected override void RegisterParameters(IParameterRegisterer registerer) {
            foreach (var registration in this.registrations)
            {
                registration(registerer);
            }
        }

        public override string BaseUrl {
            get { return this.url; }
        }
    }
}