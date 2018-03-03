namespace vidley.net
{
    public class Settings
    {
        public string ConnectionString;
        public string Database;

        public JwtSettings JwtSettings;
    }

    public class JwtSettings
    {
        public string Key;
        public string Issuer;
        public string Audience;
        public int TimeSpanMinutes;
    }
}