namespace Himmelsk.LastFmApi.Services.LastFm.Auth.Results
{
    public class AuthGetMobileSessionResult
    {
        public Session Session { get; set; }
    }

    public class Session {
        public string Name { get; set; }
        public string Key { get; set; }
        public bool Subscriber { get; set; }
    }
}