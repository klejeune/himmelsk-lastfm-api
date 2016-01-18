using Newtonsoft.Json.Serialization;

namespace Himmelsk.LastFmApi.Engine.Converters
{
    public class LowercaseContractResolver : DefaultContractResolver {
        public LowercaseContractResolver() { }

        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLowerInvariant();
        }
         
    }
}