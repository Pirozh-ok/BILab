using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.SpecialOffer {
    public class SpecialOfferDTO : BaseEntityDto<Guid> {
        public string? Detail { get; set; }
        public int Size { get; set; }
    }
}
