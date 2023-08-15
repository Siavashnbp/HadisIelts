using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class WritingCorrectionServicePrice : IEntity<int>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int WritingType { get; set; }
        public decimal Price { get; set; }
        public int WordCount { get; set; }
    }
}
