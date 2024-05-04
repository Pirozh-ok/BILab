namespace BILab.Domain.DTOs.User {
    public class AuthResponseDTO<TData> {
        public TData Data { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
