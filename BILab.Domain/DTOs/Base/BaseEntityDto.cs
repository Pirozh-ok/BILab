namespace BILab.Domain.DTOs.Base
{
    public class BaseEntityDto<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
