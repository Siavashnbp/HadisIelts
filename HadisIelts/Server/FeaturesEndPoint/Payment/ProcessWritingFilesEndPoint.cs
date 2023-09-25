using Ardalis.ApiEndpoints;
using HadisIelts.Server.Services.Files;
using HadisIelts.Server.Services.Payment;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class ProcessWritingFilesEndPoint : EndpointBaseSync
        .WithRequest<ProcessWritingFilesRequest>
        .WithActionResult<ProcessWritingFilesRequest.Response>
    {
        private readonly IWordFileServices _wordFileServices;
        private readonly IWritingCorrectionPayment _writingCorrrectionServices;
        public ProcessWritingFilesEndPoint(IWordFileServices wordFileServices
            , IWritingCorrectionPayment writingCorrectionPayment)
        {
            _wordFileServices = wordFileServices;
            _writingCorrrectionServices = writingCorrectionPayment;
        }
        [Authorize]
        [HttpPost(ProcessWritingFilesRequest.EndPointUri)]
        public override ActionResult<ProcessWritingFilesRequest.Response> Handle(ProcessWritingFilesRequest request)
        {
            try
            {
                uint price = 0;
                List<ProcessedWritingFileSharedModel> processedWritingFiles = new List<ProcessedWritingFileSharedModel>();
                foreach (var file in request.WritingFiles)
                {
                    int wordCount = 0;
                    wordCount = _wordFileServices.CountFileWords(file.Data);
                    if (wordCount <= 0)
                    {
                        processedWritingFiles.Add(new ProcessedWritingFileSharedModel
                        {
                            PriceGroup = null,
                            WritingFile = file,
                            Message = "Writing is not in correct format"
                        });
                    }
                    else
                    {
                        file.WordCount = wordCount;
                        var filePrice = _writingCorrrectionServices.CalculateFilePriceAsync(wordCount, file.WritingTypeID);
                        if (filePrice.PriceGroup.Price > 0)
                        {
                            price += filePrice.PriceGroup.Price;
                        }
                        processedWritingFiles.Add(new ProcessedWritingFileSharedModel
                        {
                            WritingFile = file,
                            PriceGroup = filePrice.PriceGroup,
                            Message = filePrice.Message
                        });
                    }
                }
                return Ok(new ProcessWritingFilesRequest.Response(new WritingCorrectionPackageSharedModel
                {
                    ProcessedWritingFiles = processedWritingFiles,
                    TotalPrice = price,
                },
                Message: string.Empty));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
