namespace BILab.Domain.Contracts.Models {
    public interface IBaseEntity<TKey> {
        TKey Id { get; set; }
    }
}