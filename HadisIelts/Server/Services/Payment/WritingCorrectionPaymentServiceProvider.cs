using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;

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
        public uint CalculateFilePriceAsync(int wordCount, int writingTypeID)
        {
            var allPrices = _writingCorrectionPriceRepository.GetAll();
            var price = allPrices.Where(x => x.WritingTypeID == writingTypeID && x.WordCount > wordCount)
                .MinBy(x => x.WordCount);
            if (price is not null)
            {
                return price.Price;
            }
            return 0;
        }
    }
}

