namespace HadisIelts.Shared.Models
{
    public class WritingFileSharedModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int WritingTypeID { get; set; }
        public string Data { get; set; }
        public int? WordCount { get; set; }
    }
}
