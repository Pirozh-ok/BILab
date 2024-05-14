using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Adress;
using BILab.Domain.Models.Entities;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class AdressService : BaseEntityService<Adress, Guid, AdressDTO>, IAdressService {
        public AdressService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }

        protected override ServiceResult Validate(AdressDTO dto) {
            var errors = new List<string>();

            if (dto is null) {
                errors.Add(ResponseConstants.NullArgument);
                return BuildValidateResult(errors);
            }

            if(dto.City.Length > Constants.MaxLenOfName) {
                errors.Add($"City name length must be less then {Constants.MaxLenOfName}");
            }

            if (dto.City.Length < Constants.MinLenOfName) {
                errors.Add($"City name length must be more then {Constants.MinLenOfName}");
            }

            if (dto.Street.Length < Constants.MinLenOfName) {
                errors.Add($"Street name length must be more then {Constants.MinLenOfName}");
            }

            if (dto.Street.Length > Constants.MaxLenOfName) {
                errors.Add($"Street name length must be less then {Constants.MaxLenOfName}");
            }

            if(dto.ApartmentNumber < 0 || dto.ApartmentNumber > 100000000) {
                errors.Add($"Apartment number must be in the range from 0 to 100000000");
            }

            return BuildValidateResult(errors);
        }
    }
}