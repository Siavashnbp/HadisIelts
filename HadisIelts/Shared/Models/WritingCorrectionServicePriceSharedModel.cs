namespace HadisIelts.Shared.Models
{
    public class WritingCorrectionServicePriceSharedModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WritingTypeId { get; set; }
        public int WordCount { get; set; }
        public uint Price { get; set; }
    }
}
