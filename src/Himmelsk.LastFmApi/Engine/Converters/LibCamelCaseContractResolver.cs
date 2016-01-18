using System.Text;
using Newtonsoft.Json.Serialization;

namespace Himmelsk.LastFmApi.Engine.Converters {
    class LibCamelCaseContractResolver : DefaultContractResolver {
        public LibCamelCaseContractResolver() { }

        protected override string ResolvePropertyName(string propertyName) {
            if (string.IsNullOrEmpty(propertyName) || !char.IsUpper(propertyName[0]))
                return propertyName;

            var stringBuilder = new StringBuilder();
            for (int startIndex = 0; startIndex < propertyName.Length; startIndex++) {
                if (char.IsUpper(propertyName[startIndex])) {
                    if (startIndex == 0) {
                        stringBuilder.Append(char.ToLowerInvariant(propertyName[startIndex]));
                    }
                    else {
                        stringBuilder.Append("_" + char.ToLowerInvariant(propertyName[startIndex]));
                    }
                }
                else {
                    stringBuilder.Append(propertyName[startIndex]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
