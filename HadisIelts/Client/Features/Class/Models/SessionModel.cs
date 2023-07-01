using HadisIelts.Client.Features.Models.Class;

namespace HadisIelts.Client.Features.Class.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Details { get; set; }
        public ClassModel Class { get; set; }
    }
}
