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

            //name city length
            //Street length
            //HouseNumber length
            //ApartmentNumber > 0 < 100000000000000000000

            //if (dto.Size < Constants.MinSpecialOffer) {
            //    errors.Add($"Special offer size must be more then {MinSpecialOffer}");
            //}

            //if (dto.Size > Constants.MaxSpecialOffer) {
            //    errors.Add($"Special offer size must be less then {Constants.MaxSpecialOffer}");
            //}

            //if (dto.Detail != null && dto.Detail.Length > Constants.MaxLenOfDetail) {
            //    errors.Add($"Special offer detail length must be less then {Constants.MaxLenOfDetail}");
            //}

            return BuildValidateResult(errors);
        }
    }
}