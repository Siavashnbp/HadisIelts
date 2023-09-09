using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class EditWritingCorrectionPriceEndpoint : EndpointBaseAsync
        .WithRequest<EditWritingCorrectionPriceRequest>
        .WithActionResult<EditWritingCorrectionPriceRequest.Response>
    {
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int> _writingCorrectionPriceRepository;
        public EditWritingCorrectionPriceEndpoint(
            ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionPriceRepository)
        {
            _writingCorrectionPriceRepository = writingCorrectionPriceRepository;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(EditWritingCorrectionPriceRequest.EndpointUri)]
        public override async Task<ActionResult<EditWritingCorrectionPriceRequest.Response>> HandleAsync(EditWritingCorrectionPriceRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var writingCorrectionPriceEntity =
                    await _writingCorrectionPriceRepository.FindByIDAsync(request.WritingCorrectionPrice.ID);
                if (writingCorrectionPriceEntity is not null)
                {
                    writingCorrectionPriceEntity.Name = request.WritingCorrectionPrice.Name;
                    writingCorrectionPriceEntity.Price = request.WritingCorrectionPrice.Price;
                    writingCorrectionPriceEntity.WordCount = request.WritingCorrectionPrice.WordCount;
                    writingCorrectionPriceEntity.WritingTypeID = request.WritingCorrectionPrice.WritingTypeID;
                    var result = _writingCorrectionPriceRepository.Update(writingCorrectionPriceEntity);
                    if (result)
                    {
                        return Ok(new EditWritingCorrectionPriceRequest.Response(
                            new Shared.Models.WritingCorrectionServicePriceSharedModel
                            {
                                ID = writingCorrectionPriceEntity.ID,
                                Name = writingCorrectionPriceEntity.Name,
                                Price = writingCorrectionPriceEntity.Price,
                                WordCount = writingCorrectionPriceEntity.WordCount,
                                WritingTypeID = writingCorrectionPriceEntity.WritingTypeID
                            }));
                    }
                }
                return Problem();
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
