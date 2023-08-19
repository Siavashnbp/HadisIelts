namespace HadisIelts.Server.Services.Payment
{
    public interface IWritingCorrectionPayment
    {
        public uint CalculateFilePriceAsync(int wordCount, int writingTypeID);
    }

}
