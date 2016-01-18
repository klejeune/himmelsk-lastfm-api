using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Himmelsk.LastFmApi.Services.LastFm.User.Results {
    public class RecentTracks {
        [JsonProperty(PropertyName = "@attr")]
        public RecentTracksAttributes Attributes { get; set; }

        public IEnumerable<Track> Track { get; set; }
    }
}