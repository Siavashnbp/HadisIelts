using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class WritingPaymentPicture : IEntity<string>

    {
        public string ID { get; set; }
        public string Data { get; set; }
        public string SubmitedWritingCorrectionFilesID { get; set; }
        public SubmittedWritingCorrectionFiles SubmittedWritingCorrectionFiles { get; set; }
    }
}
