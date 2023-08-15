using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
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
        public AddWritingCorrectionPriceEndPoint
            (ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionRepository)
        {
            _writingCorrectionRepository = writingCorrectionRepository;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(AddWritingCorrectionPriceRequest.EndPointUri)]
        public override async Task<ActionResult<AddWritingCorrectionPriceRequest.Response>> HandleAsync(AddWritingCorrectionPriceRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                //validate request
                if (request.Request is not null)
                {
                    var writingCorrectionPriceEntity = new WritingCorrectionServicePrice
                    {
                        Name = request.Request.Name,
                        Price = request.Request.Price,
                        WordCount = request.Request.WordCount,
                        WritingType = request.Request.WritingType
                    };
                    var id = await _writingCorrectionRepository.InsertAsync(writingCorrectionPriceEntity);
                    var createdEntity = await _writingCorrectionRepository.FindByIDAsync(id);
                    if (createdEntity is not null)
                    {
                        return Ok(new AddWritingCorrectionPriceRequest.Response(new WritingCorrectionPrice
                        {
                            Name = createdEntity.Name,
                            Price = createdEntity.Price,
                            WordCount = createdEntity.WordCount,
                            WritingType = createdEntity.WritingType
                        }));
                    }
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
