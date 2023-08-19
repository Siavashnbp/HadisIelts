using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class WritingCorrectionServicePrice : IEntity<int>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int WritingTypeID { get; set; }
        public ApplicationWritingType WritingType { get; set; }
        public uint Price { get; set; }
        public int WordCount { get; set; }
    }
}
