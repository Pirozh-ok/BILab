using BILab.Domain.DTOs.Base;

namespace BILab.Domain.DTOs.Adress {
    public class AdressDTO : BaseEntityDto<Guid> {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
    }
}
