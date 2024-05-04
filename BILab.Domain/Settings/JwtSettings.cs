namespace BILab.Domain.Settings {
    public class JwtSettings {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenValidityInSecond { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
        public string Key { get; set; }
    }
}
