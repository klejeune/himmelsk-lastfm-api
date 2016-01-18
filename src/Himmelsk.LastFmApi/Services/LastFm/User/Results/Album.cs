using Newtonsoft.Json;

namespace Himmelsk.LastFmApi.Services.LastFm.User.Results {
    public class Album {
        [JsonProperty(PropertyName = "#text")]
        public string Name { get; set; }
        public string Mbid { get; set; }
    }
}