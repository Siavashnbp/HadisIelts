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
                    decimal price = 0;
                    List<ProcessedWritingFile> processedWritingFiles = new List<ProcessedWritingFile>();
                    foreach (var file in request.Request.WritingFiles)
                    {
                        int wordCount = 0;
                        wordCount = _wordFileServices.CountFileWords(file.Data);
                        decimal filePrice = await _writingCorrrectionServices.CalculateFilePriceAsync(wordCount, file.WritingType);
                        processedWritingFiles.Add(new ProcessedWritingFile
                        {
                            WritingFile = file,
                            Price = filePrice,
                            WordCount = wordCount
                        });
                    }
                    return Ok(new ProcessWritingFilesRequest.Response(new CalculatedPayment
                    {
                        ProcessedFiles = processedWritingFiles,
                        Message = "Files were processed successfully"
                    }));
                }
                return BadRequest(new ProcessWritingFilesRequest.Response(new CalculatedPayment
                {
                    ProcessedFiles = null,
                    Message = "No files were submited"
                }));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
