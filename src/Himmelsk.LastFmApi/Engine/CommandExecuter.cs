using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Himmelsk.LastFmApi.Engine.Results;
using Himmelsk.Social.Api.Core.Common;
using Himmelsk.Social.Api.Core.LinkedIn.Results;

namespace Himmelsk.LastFmApi.Engine {
    class CommandExecuter<TCredentials> : ICommandExecuter<TCredentials> where TCredentials : ICredentials {
        public async Task<IResult<TResult>> ExecuteAsync<TResult>(TCredentials credentials, ICommand<TResult> command) {
            string result;

            command.LoadParameters(credentials);
            var uri = this.GetFullUri(credentials, command);
            uri = this.AlterUri(credentials, command, uri);
            var hwr = (HttpWebRequest)WebRequest.Create(uri);
            hwr.Method = this.GetVerb(command.Verb);

            credentials.ApplyToRequest(command, hwr);

            if (!string.IsNullOrEmpty(command.ContentType)) {
                hwr.ContentType = command.ContentType;
            }


            this.AddHeader(credentials, command, hwr);

            command.AddHeaders((key, value) => hwr.Headers[key] = value);

            //GS - POST off the request

            //hwr.ContentType = "application/x-www-form-urlencoded";

            if (command.Verb == CommandVerb.Post) {
                byte[] bodyBytes = Encoding.UTF8.GetBytes(this.GetPost(command));

                using (var requestStream = await Task<Stream>.Factory.FromAsync(hwr.BeginGetRequestStream, hwr.EndGetRequestStream, hwr)) {
                    await requestStream.WriteAsync(bodyBytes, 0, bodyBytes.Length);
                }
            }
            else {
                if (!string.IsNullOrEmpty(this.GetPost(command))) {
                    throw new InvalidOperationException("Cannot send command with GET verb and POST parameters");
                }
            }
            
            int statusCode;


            //var a =
            //       AsyncHelpers.RunSync<WebResponse>(() => Task<WebResponse>.Factory.FromAsync(hwr.BeginGetResponse, hwr.EndGetResponse, hwr));


            using (var stream = await Task<WebResponse>.Factory.FromAsync(hwr.BeginGetResponse, hwr.EndGetResponse, hwr)) {
                using (Stream responseStream = stream.GetResponseStream()) {

                    var sr = new StreamReader(responseStream);
                    result = await sr.ReadToEndAsync();

                }

                statusCode = (int)((HttpWebResponse)stream).StatusCode;

            }





            // WebResponse responseObject =  Task<WebResponse>.Factory.FromAsync(hwr.BeginGetResponse, hwr.EndGetResponse, hwr);

            //var responseO = Task<WebResponse>.Factory.FromAsync(hwr.BeginGetResponse, hwr.EndGetResponse, hwr);
            //var responseObject = await responseO;

            //var responseStream = responseObject.GetResponseStream();
            //var sr = new StreamReader(responseStream);
            //result = await sr.ReadToEndAsync();
            //statusCode = (int)((HttpWebResponse)responseObject).StatusCode;


            Error error = null;
            TResult formattedResult = default(TResult);

            if (statusCode == 500 || statusCode == 403 || statusCode == 401) {
                throw new ApiErrorException(new Result<Error> {
                    Raw = result,
                    Typed = command.FormatError(result, hwr.ContentType),
                });
            }
            else {
                formattedResult = command.FormatResult(result, hwr.ContentType);
            }



            return new Result<TResult> {
                Raw = result,
                Typed = formattedResult,
            };
        }



        protected virtual string AlterUri<TResult>(ICredentials credentials, ICommand<TResult> command, string uri) {
            return uri;
        }

        protected virtual void AddHeader<TResult>(ICredentials credentials, ICommand<TResult> command, HttpWebRequest request) {

        }

        protected IDictionary<string, string> GetAllParameters(ICommand command) {
            return command.GetParameters
                .Union(command.PostParameters)
                .OrderBy(pair => pair.Key)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private string GetVerb(CommandVerb verb) {
            switch (verb) {
                case CommandVerb.Get:
                    return "GET";
                case CommandVerb.Post:
                    return "POST";
                default:
                    throw new NotImplementedException("Verb " + verb + " is not implemented.");
            }
        }

        private string GetFullUri(ICredentials credentials, ICommand command) {
            var parameters = command.GetParameters.ToList();
            var url = credentials.Endpoint + "/" + command.BaseUrl;

            if (parameters.Any()) {
                url += "?" + string.Join("&", parameters.Select(pair => pair.Key + '=' + pair.Value));
            }

            return url;
        }

        private string GetPost(ICommand command) {
            if (command.PostParameters.Any() && !string.IsNullOrEmpty(command.PostContent)) {
                throw new InvalidOperationException("You cannot use post parameters and define a post content. Use only one of these options.");
            }

            if (command.PostParameters.Any()) {
                return string.Join("&", command.PostParameters.OrderBy(p => p.Key).Select(pair => pair.Key + '=' + pair.Value));
            }

            if (!string.IsNullOrEmpty(command.PostContent)) {
                return command.PostContent;
            }

            return string.Empty;
        }

        public async Task<IResult<TResult>> Execute<TResult>(TCredentials credentials, ICommand<TResult> command) {

            var task = await this.ExecuteAsync(credentials, command);
            return task;

        }
    }
}