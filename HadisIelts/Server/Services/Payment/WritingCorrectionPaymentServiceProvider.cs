using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Server.Services.Payment
{
    public class WritingCorrectionPaymentServiceProvider : IWritingCorrectionPayment
    {
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int> _writingCorrectionPriceRepository;
        public WritingCorrectionPaymentServiceProvider(
            ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionPriceRepository)
        {
            _writingCorrectionPriceRepository = writingCorrectionPriceRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordCount">file word count submitted by user and processed by WordFileServiceProvider</param>
        /// <param name="writingTypeID">task selected by user</param>
        /// <returns>ProcessedWritingFile with no message if price is found or appropriate message for errors</returns>
        public ProcessedWritingFile CalculateFilePriceAsync(int wordCount, int writingTypeID)
        {
            var allPrices = _writingCorrectionPriceRepository.GetAll();
            if (allPrices is null)
            {
                return new ProcessedWritingFile
                {
                    Message = "Writing task was not found"
                };
            }
            var price = allPrices.Where(x => x.WritingTypeID == writingTypeID && x.WordCount > wordCount)
                .MinBy(x => x.WordCount);
            if (price is not null)
            {
                return new ProcessedWritingFile
                {
                    PriceGroup = new PriceGroup
                    {
                        Price = price.Price,
                        PriceName = price.Name
                    },
                    WordCount = wordCount,
                };
            }
            return new ProcessedWritingFile
            {
                Message = $"Writing word count exceeds maximum word count for this task",
                WordCount = wordCount,
            };
        }
    }
}

