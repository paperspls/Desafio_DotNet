namespace Geo_WebApi_ASP.NET.Security
{
    public class Settings
    {
        private static string secret = "6e60a063e34be4515cb4d3b2b1c1a621a0436efe9dd4279ffe37ee4a81d865ff";

        public static string Secret { get => secret; set => secret = value; }
    }
}
