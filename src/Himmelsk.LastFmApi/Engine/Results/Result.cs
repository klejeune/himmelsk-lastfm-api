namespace Himmelsk.LastFmApi.Engine.Results {
    class Result<TResult> : IResult<TResult> {
        public TResult Typed { get; set; }
        public string Raw { get; set; }
    }
}
