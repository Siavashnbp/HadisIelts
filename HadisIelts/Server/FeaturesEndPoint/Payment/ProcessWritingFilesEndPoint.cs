using Ardalis.ApiEndpoints;
using HadisIelts.Server.Services.Files;
using HadisIelts.Server.Services.Payment;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class ProcessWritingFilesEndPoint : EndpointBaseAsync
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
        [HttpPost(ProcessWritingFilesRequest.EndPointUri)]
        public override async Task<ActionResult<ProcessWritingFilesRequest.Response>> HandleAsync(ProcessWritingFilesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request.Request.WritingFiles is not null
                    && request.Request.WritingFiles.Count > 0)
                {
                    uint price = 0;
                    List<ProcessedWritingFile> processedWritingFiles = new List<ProcessedWritingFile>();
                    foreach (var file in request.Request.WritingFiles)
                    {
                        int wordCount = 0;
                        wordCount = _wordFileServices.CountFileWords(file.Data);
                        if (wordCount <= 0)
                        {
                            processedWritingFiles.Add(new ProcessedWritingFile
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
                                processedWritingFiles.Add(new ProcessedWritingFile
                                {
                                    WritingFile = file,
                                    PriceGroup = filePrice.PriceGroup,
                                    Message = filePrice.Message
                                });
                            }
                        }

                    }
                    return Ok(new ProcessWritingFilesRequest.Response(new CalculatedWritingCorrectionPayment
                    {
                        ProcessedFiles = processedWritingFiles,
                        TotalPrice = price,
                        Message = "Files were processed successfully"
                    }));
                }
                return BadRequest(new ProcessWritingFilesRequest.Response(new CalculatedWritingCorrectionPayment
                {
                    ProcessedFiles = null,
                    Message = "No files were submited"
                }));
            }
            catch (Exception)
            {
                return Problem("Something went wrong");
            }
        }
    }
}
