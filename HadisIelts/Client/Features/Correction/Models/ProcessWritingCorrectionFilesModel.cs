using HadisIelts.Shared.Models;

namespace HadisIelts.Client.Features.Correction.Models
{
    public class ProcessWritingCorrectionFilesModel
    {
        public List<WritingFileSharedModel> WritingFiles { get; set; }
        public bool IsProcessed { get; set; }
        public ProcessWritingCorrectionFilesModel()
        {
            WritingFiles = new List<WritingFileSharedModel>();
        }
    }
}
