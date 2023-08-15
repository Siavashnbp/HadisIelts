using static HadisIelts.Shared.Enums.FileRelatedEnums;

namespace HadisIelts.Server.Services.Payment
{
    public interface IWritingCorrectionPayment
    {
        public Task<decimal> CalculateFilePriceAsync(int wordCount, WritingTypes writingType);
    }

}
