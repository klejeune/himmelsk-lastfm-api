using Newtonsoft.Json.Serialization;

namespace Himmelsk.LastFmApi.Engine.Converters {
    class LowerHyphenJsonConverter : DefaultContractResolver {
        public LowerHyphenJsonConverter()
            : base(true) { }

        protected override string ResolvePropertyName(string propertyName) {
            if (char.IsUpper(propertyName[0])) {
                if (propertyName.Length == 1) {
                    return char.ToLowerInvariant(propertyName[0]).ToString();
                }
                else {
                    return char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
                }
            }

            return propertyName;
        }
    }
}
