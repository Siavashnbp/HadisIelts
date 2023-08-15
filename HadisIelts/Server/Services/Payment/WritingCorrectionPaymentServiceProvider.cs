using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using static HadisIelts.Shared.Enums.FileRelatedEnums;
using static HadisIelts.Shared.Enums.PaymentRelatedEnums;

namespace HadisIelts.Server.Services.Payment
{
    public class WritingCorrectionPaymentServiceProvider : IWritingCorrectionPayment
    {
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int> _repositoryServices;
        public WritingCorrectionPaymentServiceProvider(ICustomRepositoryServices<WritingCorrectionServicePrice, int> repositoryServices)
        {
            _repositoryServices = repositoryServices;
        }
        public async Task<decimal> CalculateFilePriceAsync(int wordCount, WritingTypes writingType)
        {
            int paymenID = 0;
            switch (writingType)
            {
                case WritingTypes.IeltsTask1:
                    if (wordCount > 0 && wordCount < 200)
                    {
                        paymenID = (int)WritingCorrectionWordCountPriceID.Task1UpTo200Words;
                    }
                    else
                    {
                        paymenID = -1;
                    }
                    break;
                case WritingTypes.IeltsTask2:
                    if (wordCount <= 0 || wordCount > 400)
                    {
                        paymenID = -1;
                    }
                    else if (wordCount < 300)
                    {
                        paymenID = (int)WritingCorrectionWordCountPriceID.Task2UpTo300Words;
                    }
                    else if (wordCount < 350)
                    {
                        paymenID = (int)WritingCorrectionWordCountPriceID.Task2UpTo350Words;
                    }
                    else
                    {
                        paymenID = (int)WritingCorrectionWordCountPriceID.Task2UpTo400Words;
                    }
                    break;
                default:
                    paymenID = -1;
                    break;
            }
            if (paymenID > 0)
            {
                var servicePrice = await _repositoryServices.FindByIDAsync(paymenID);
                return servicePrice.Price;
            }
            return -1;

        }
    }
}

