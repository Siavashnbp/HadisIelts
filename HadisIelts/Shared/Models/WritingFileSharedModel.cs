namespace HadisIelts.Shared.Models
{
    public class WritingFileSharedModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WritingTypeId { get; set; }
        public string Data { get; set; }
        public int? WordCount { get; set; }
    }
}
