using HadisIelts.Shared.Models;

namespace HadisIelts.Client.Features.Correction.Models
{
    public class WritingFileModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FileData { get; set; }
        public long FileSize { get; set; }
        public WritingTypeSharedModel WritingType { get; set; }
    }
}
