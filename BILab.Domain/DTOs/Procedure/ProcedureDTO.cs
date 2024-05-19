using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.Procedure {
    public class ProcedureDTO : BaseEntityDto<Guid> {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public Guid? SpecialOfferId { get; set; }
        public string? Picture { get; set; }
    }
}
