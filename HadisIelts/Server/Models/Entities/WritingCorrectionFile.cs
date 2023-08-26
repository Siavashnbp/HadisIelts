using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class WritingCorrectionFile : IEntity<int>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public int WordCount { get; set; }
        public uint Price { get; set; }
        public int ApplicationWritingTypeID { get; set; }
        public ApplicationWritingType ApplicationWritingType { get; set; }
        public string SubmittedWritingCorecionFilesID { get; set; }
        public SubmittedWritingCorrectionFiles SubmittedWritingCorrectionFiles { get; set; }
    }
}
