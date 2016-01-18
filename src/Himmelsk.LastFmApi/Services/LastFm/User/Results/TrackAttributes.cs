using Himmelsk.LastFmApi.Engine.Converters;
using Newtonsoft.Json;

namespace Himmelsk.LastFmApi.Services.LastFm.User.Results {
    public class TrackAttributes {
        [JsonConverter(typeof(Bool01JsonConverter))]
        public bool NowPlaying { get; set; }
    }
}