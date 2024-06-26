﻿using AutoMapper;
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
            var errors = new List<string>();

            if (dto is null) {
                errors.Add(ResponseConstants.NullArgument);
                return BuildValidateResult(errors);
            }

            if (dto.NewPrice > 1000000 || dto.NewPrice < 0 ) {
                errors.Add($"Special offer price is not corrected");
            }

            if (dto.Detail != null && dto.Detail.Length > Constants.MaxLenOfDetail) {
                errors.Add($"Special offer detail length must be less then {Constants.MaxLenOfDetail}");
            }

            return BuildValidateResult(errors);
        }
    }
}