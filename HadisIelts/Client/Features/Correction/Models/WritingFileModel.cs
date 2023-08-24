using HadisIelts.Client.Features.Teacher.Models;

namespace HadisIelts.Client.Features.Correction.Models
{
    public class WritingFileModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FileData { get; set; }
        public long FileSize { get; set; }
        public WritingTypeModel WritingType { get; set; }
    }
}
