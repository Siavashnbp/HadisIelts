﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.General
{
    public class GetWritingCorrectionServicePrices : EndpointBaseSync
        .WithoutRequest
        .WithActionResult<GetWritingCorrectionPricesRequest.Response>
    {
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int> _writingCorrectionPriceRepository;
        public GetWritingCorrectionServicePrices(
            ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionRepository)
        {
            _writingCorrectionPriceRepository = writingCorrectionRepository;
        }

        [HttpGet(GetWritingCorrectionPricesRequest.EndPointUri)]
        public override ActionResult<GetWritingCorrectionPricesRequest.Response> Handle()
        {
            try
            {
                var prices = _writingCorrectionPriceRepository.GetAll();
                var mapper = new Mapper(new MapperConfiguration(config =>
                {
                    config.CreateMap<WritingCorrectionServicePrice, WritingCorrectionServicePriceSharedModel>();
                }));
                var result = new List<WritingCorrectionServicePriceSharedModel>();
                foreach (var item in prices)
                {
                    result.Add(mapper.Map<WritingCorrectionServicePriceSharedModel>(item));
                }
                return Ok(new GetWritingCorrectionPricesRequest.Response(result));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
