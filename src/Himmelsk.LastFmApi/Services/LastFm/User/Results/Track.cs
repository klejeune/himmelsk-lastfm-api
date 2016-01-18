using Himmelsk.LastFmApi.Engine.Converters;
using Newtonsoft.Json;

namespace Himmelsk.LastFmApi.Services.LastFm.User.Results {
    public class Track {
        [JsonProperty(PropertyName = "@attr")]
        public TrackAttributes Attributes { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(Bool01JsonConverter))]
        public bool Streamable { get; set; }
        public string Mbid { get; set; }
        public string Url { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public Date Date { get; set; }

        public override string ToString() {
            return this.Artist + " - " + this.Name;
        }
    }
}