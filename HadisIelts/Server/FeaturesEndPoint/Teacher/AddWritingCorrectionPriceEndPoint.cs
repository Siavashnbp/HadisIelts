using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class AddWritingCorrectionPriceEndPoint : EndpointBaseAsync
        .WithRequest<AddWritingCorrectionPriceRequest>
        .WithActionResult<AddWritingCorrectionPriceRequest.Response>
    {
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int> _writingCorrectionRepository;
        private readonly ICustomRepositoryServices<ApplicationWritingType, int> _writingTypeRepository;
        public AddWritingCorrectionPriceEndPoint
            (ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionRepository
            , ICustomRepositoryServices<ApplicationWritingType, int> writingTypeRepository)
        {
            _writingCorrectionRepository = writingCorrectionRepository;
            _writingTypeRepository = writingTypeRepository;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(AddWritingCorrectionPriceRequest.EndPointUri)]
        public override async Task<ActionResult<AddWritingCorrectionPriceRequest.Response>> HandleAsync(AddWritingCorrectionPriceRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var writingType = await _writingTypeRepository.FindByIdAsync
                    (request.WritingCorrectionServicePrice.WritingTypeId);
                if (writingType is not null)
                {
                    var writingCorrectionPriceEntity = new WritingCorrectionServicePrice
                    {
                        Name = request.WritingCorrectionServicePrice.Name,
                        Price = request.WritingCorrectionServicePrice.Price,
                        WordCount = request.WritingCorrectionServicePrice.WordCount,
                        WritingType = writingType
                    };
                    var addedWritingCorrectionPrice = _writingCorrectionRepository.Insert(writingCorrectionPriceEntity);
                    return Ok(new AddWritingCorrectionPriceRequest.Response(
                        new WritingCorrectionServicePriceSharedModel
                        {
                            Id = addedWritingCorrectionPrice.Id,
                            Name = addedWritingCorrectionPrice.Name,
                            Price = addedWritingCorrectionPrice.Price,
                            WordCount = addedWritingCorrectionPrice.WordCount,
                            WritingTypeId = addedWritingCorrectionPrice.WritingTypeId
                        }));

                }
                return Problem();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
