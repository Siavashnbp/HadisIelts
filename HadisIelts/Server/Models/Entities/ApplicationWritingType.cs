using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class ApplicationWritingType : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WritingCorrectionServicePrice> WritingCorrectionServicePrices { get; set; }
        public List<WritingCorrectionFile> WritingCorrectionFiles { get; set; }
    }
}
