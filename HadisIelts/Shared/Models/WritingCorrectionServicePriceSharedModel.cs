namespace HadisIelts.Shared.Models
{
    public class WritingCorrectionServicePriceSharedModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int WritingTypeID { get; set; }
        public int WordCount { get; set; }
        public uint Price { get; set; }
    }
}
