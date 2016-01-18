using System;
using Newtonsoft.Json;

namespace Himmelsk.LastFmApi.Engine.Converters
{
    public class DateTimeUtsJsonConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan span = ((DateTime)value).ToUniversalTime() - baseDate;

            writer.WriteValue((long)span.TotalSeconds);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Convert.ToInt64(reader.Value)).ToLocalTime();
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(bool);
        }
    }
}