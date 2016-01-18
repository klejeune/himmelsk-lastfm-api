using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Himmelsk.LastFmApi.Engine;
using Himmelsk.LastFmApi.Engine.Converters;
using Himmelsk.Social.Api.Core.LinkedIn.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Himmelsk.Social.Api.Core.Common {
    abstract class AbstractCommand<TResult> : ICommand<TResult> where TResult : class {
        public abstract CommandVerb Verb { get; }
        protected abstract void RegisterParameters(IParameterRegisterer registerer);

        private ParameterRegisterer parameters;

        public void LoadParameters(ICredentials credentials)
        {
            this.parameters = new ParameterRegisterer();
            this.RegisterParameters(this.parameters);
            credentials.RegisterParameters(this, this.parameters);
        }

        public abstract string BaseUrl { get; }

        public IEnumerable<IParameter> Parameters {
            get {
                return parameters.Parameters.Select(
                      pair => new Parameter { Key = pair.Key, Value = pair.Value });
            }
        }

        public IEnumerable<IParameter> PostParameters {
            get {
                return
                    parameters.ParametersPost.Select(
                        pair => new Parameter { Key = pair.Key, Value = pair.Value });
            }
        }

        public string PostContent {
            get {
                return
                    parameters.PostContent;
            }
        }

        public IEnumerable<IParameter> GetParameters {
            get {
                return
                    parameters.ParametersGet.Select(
                        pair => new Parameter { Key = pair.Key, Value = pair.Value });
            }
        }

        public virtual string ContentType { get { return "application/json"; } }

        public virtual void AddHeaders(Action<string, string> add) {
        }

        protected virtual T FormatResult<T>(string value, string contentType) where T : class {
            T result;

            if (typeof(T) == typeof (string)) {
                return value as T;
            }
            else if (contentType.StartsWith("application/json") || contentType.StartsWith("text/javascript")) {
                var settings = this.CreateJsonSettings();

                result = JsonConvert.DeserializeObject<T>(value, settings);
            }
            else if (false) {
                var serializer = new XmlSerializer(typeof(T));

                using (var reader = new StringReader(value)) {
                    result = (T)serializer.Deserialize(reader);
                }
            }
            else {
                throw new InvalidOperationException("Response Content Type not supported: " + contentType);
            }

            return result;
        }

        protected virtual JsonSerializerSettings CreateJsonSettings() {
            return new JsonSerializerSettings {
                ContractResolver = new LowercaseContractResolver(),
                
            };
        }

        public TResult FormatResult(string value, string contentType) {
            return this.FormatResult<TResult>(value, contentType);
        }

        public Error FormatError(string value, string contentType) {
            return this.FormatResult<Error>(value, contentType);
        }

        private class ParameterRegisterer : IParameterRegisterer {
            public ParameterRegisterer() {
                ParametersPost = new Dictionary<string, string>();
                ParametersGet = new Dictionary<string, string>();
            }

            public IDictionary<string, string> ParametersPost { get; private set; }
            public IDictionary<string, string> ParametersGet { get; private set; }
            public string PostContent { get; private set; }

            public void RegisterGet(string name, string value) {
                if (!string.IsNullOrEmpty(value)) {
                    this.ParametersGet.Add(name, System.Uri.EscapeDataString(value));
                }
            }

            public void RegisterPost(string name, string value) {
                if (!string.IsNullOrEmpty(value)) {
                    this.ParametersPost.Add(name, System.Uri.EscapeDataString(value));
                }
            }

            public IDictionary<string, string> Parameters {
                get
                {
                    return this.ParametersGet
                        .Union(this.ParametersPost)
                        .OrderBy(pair => pair.Key)
                        .ToDictionary(pair => pair.Key, pair => pair.Value);
                }
            }

            public void RegisterGet(string name, bool value) {
                this.RegisterGet(name, value.ToString().ToLowerInvariant());
            }

            public void RegisterGet(string name, bool? value) {
                if (value.HasValue)
                {
                    this.RegisterGet(name, value.ToString().ToLowerInvariant());
                }
            }

            public void RegisterPost(string name, bool value) {
                this.RegisterPost(name, value.ToString().ToLowerInvariant());
            }

            public void RegisterPost(string name, bool? value) {
                if (value.HasValue) {
                    this.RegisterPost(name, value.ToString().ToLowerInvariant());
                }
            }

            public void RegisterGet(string name, int? value) {
                if (value.HasValue) {
                    this.RegisterGet(name, value.Value.ToString(CultureInfo.InvariantCulture));
                }
            }

            public void RegisterPost(string name, int? value) {
                if (value.HasValue) {
                    this.RegisterPost(name, value.Value.ToString(CultureInfo.InvariantCulture));
                }
            }

            public void RegisterGet(string name, DateTime? value, string format) {
                if (value.HasValue) {
                    this.RegisterGet(name, value.Value.ToString(format));
                }
            }

            public void RegisterPost(string name, DateTime? value, string format) {
                if (value.HasValue) {
                    this.RegisterPost(name, value.Value.ToString(format));
                }
            }

            private static IDictionary<JsonMode, DefaultContractResolver> Converters = new Dictionary<JsonMode, DefaultContractResolver> {
                {
                    JsonMode.LowerHyphen, new LowerHyphenJsonConverter()
                }
            };

            public void RegisterPostContent(dynamic value) {
                var settings = new JsonSerializerSettings {
                    ContractResolver = Converters[this.PostContentMode],
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                };

                this.PostContent = JsonConvert.SerializeObject(value, settings);
            }

            protected virtual JsonMode PostContentMode {
                get {
                    return JsonMode.LowerHyphen;
                }
            }

            protected enum JsonMode {
                LowerHyphen,
            }


            public void Register(string name, string value, CommandVerb verb) {
                  if (verb == CommandVerb.Get) {
                    this.RegisterGet(name, value);
                }
                else {
                    this.RegisterPost(name, value);
                }
            }

            public void Register(string name, bool value, CommandVerb verb) {
                if (verb == CommandVerb.Get) {
                    this.RegisterGet(name, value);
                }
                else {
                    this.RegisterPost(name, value);
                }
            }

            public void Register(string name, bool? value, CommandVerb verb) {
                if (verb == CommandVerb.Get) {
                    this.RegisterGet(name, value);
                }
                else {
                    this.RegisterPost(name, value);
                }
            }

            public void Register(string name, int? value, CommandVerb verb) {
                if (verb == CommandVerb.Get) {
                    this.RegisterGet(name, value);
                }
                else {
                    this.RegisterPost(name, value);
                }
            }

            public void Register(string name, DateTime? value, string format, CommandVerb verb) {
                if (verb == CommandVerb.Get)
                {
                    this.RegisterGet(name, value, format);
                }
                else
                {
                    this.RegisterPost(name, value, format);
                }
            }
        }
    }
}
