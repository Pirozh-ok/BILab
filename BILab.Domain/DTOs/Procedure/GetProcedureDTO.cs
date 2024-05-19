using BILab.Domain.DTOs.Base;
using BILab.Domain.DTOs.SpecialOffer;

namespace BILab.Domain.DTOs.Procedure {
    public class GetProcedureDTO : BaseEntityDto<Guid> {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public SpecialOfferDTO? SpecialOffer { get; set; }
        public string? Picture { get; set; }
    }
}
