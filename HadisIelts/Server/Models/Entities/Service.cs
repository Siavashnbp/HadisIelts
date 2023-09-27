using HadisIelts.Server.Models.BaseModels;

namespace HadisIelts.Server.Models.Entities
{
    public class Service : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PaymentGroup> PaymentGroups { get; set; }
    }
}
