namespace Himmelsk.LastFmApi.Engine.Results {
    public interface IResult<out TResult> {
        TResult Typed { get; }
        string Raw { get; }
    }
}
