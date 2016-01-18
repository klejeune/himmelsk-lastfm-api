using Newtonsoft.Json;

namespace Himmelsk.LastFmApi.Services.LastFm.User.Results
{
    public class Artist
    {
        public string Mbid { get; set; }
        [JsonProperty(PropertyName = "#text")]
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}