namespace BILab.Domain.Contracts.Models {
    public interface IAuditableEntity : IBaseEntity<Guid> {
        DateTime CreatedAt { get; set; }
        string? CreatedBy { get; set; }
        DateTime ModifiedAt { get; set; }
        string? ModifiedBy { get; set; }
        DateTime? DeletedAt { get; set; }
        string? DeletedBy { get; set; }
        bool IsDeleted { get; set; }
    }

}
