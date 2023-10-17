namespace HadisIelts.Client.Features.Models.Class
{
    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DayOfWeek> Days { get; set; }
        public TimeOnly Time { get; set; }
        public List<StudentModel> Students { get; set; }
    }
}
