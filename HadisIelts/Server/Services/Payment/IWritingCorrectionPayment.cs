using HadisIelts.Shared.Models;

namespace HadisIelts.Server.Services.Payment
{
    public interface IWritingCorrectionPayment
    {
        public ProcessedWritingFileSharedModel CalculateFilePriceAsync(int wordCount, int writingTypeID);
    }

}
