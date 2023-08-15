using Ardalis.ApiEndpoints;
using AutoMapper;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class GetWritingCorrectionServicePrices : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetWritingCorrectionPricesRequest.Response>
    {
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int> _writingCorrectionPriceRepository;
        public GetWritingCorrectionServicePrices(ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionRepository
            , ApplicationDbContext dbContext)
        {
            _writingCorrectionPriceRepository = writingCorrectionRepository;
        }
        [HttpPost(GetWritingCorrectionPricesRequest.EndPointUri)]
        public override async Task<ActionResult<GetWritingCorrectionPricesRequest.Response>> HandleAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var prices = _writingCorrectionPriceRepository.GetAll();
                var mapper = new Mapper(new MapperConfiguration(config =>
                {
                    config.CreateMap<WritingCorrectionServicePrice, WritingCorrectionPrice>();
                }));
                var result = new List<WritingCorrectionPrice>();
                foreach (var item in prices)
                {
                    result.Add(mapper.Map<WritingCorrectionPrice>(item));
                }
                return Ok(new GetWritingCorrectionPricesRequest.Response(result));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
