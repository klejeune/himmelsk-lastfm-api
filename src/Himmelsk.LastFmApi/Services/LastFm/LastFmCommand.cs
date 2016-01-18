using Himmelsk.LastFmApi.Engine;
using Himmelsk.LastFmApi.Engine.Commands;
using Himmelsk.Social.Api.Core.Common;

namespace Himmelsk.LastFmApi.Services.LastFm
{
    class LastFmCommand<TResult> : GenericCommand<TResult> where TResult : class
    {
        public bool IsSigned { get; private set; }

        public LastFmCommand(string baseUrl, bool signed) : base(baseUrl, CommandVerb.Post)
        {
            this.IsSigned = signed;
        }

        protected override void RegisterParameters(IParameterRegisterer registerer)
        {
            base.RegisterParameters(registerer);

            registerer.RegisterGet("format", "json");
        }
    }
}