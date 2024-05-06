using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.SpecialOffer;
using BILab.Domain.Models.Entities;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class SpecialOfferService : BaseEntityService<SpecialOffer, Guid, SpecialOfferDTO>, ISpecialOfferService {
        public SpecialOfferService(ApplicationDbContext context, IMapper mapper) : base(context, mapper) {
        }

        protected override ServiceResult Validate(SpecialOfferDTO dto) {
            // validate 0 < size < 100;
            //validate len Detail
            return ServiceResult.Ok();
        }
    }
}
