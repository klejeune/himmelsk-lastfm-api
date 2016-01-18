using System;
using Himmelsk.LastFmApi.Engine.Converters;
using Newtonsoft.Json;

namespace Himmelsk.LastFmApi.Services.LastFm.User.Results
{
    public class Date
    {
        [JsonProperty(PropertyName = "#text")]
        public string Label { get; set; }
        [JsonConverter(typeof(DateTimeUtsJsonConverter))]
        public DateTime Uts { get; set; }
    }
}