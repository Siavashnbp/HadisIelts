using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Server.Services.Payment
{
    public interface IWritingCorrectionPayment
    {
        public ProcessedWritingFile CalculateFilePriceAsync(int wordCount, int writingTypeID);
    }

}
