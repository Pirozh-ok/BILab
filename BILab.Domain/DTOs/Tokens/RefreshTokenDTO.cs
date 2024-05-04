namespace BILab.Domain.DTOs.Tokens
{
    public class RefreshTokenDTO
    {
        public Guid Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}