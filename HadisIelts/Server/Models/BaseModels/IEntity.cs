namespace HadisIelts.Server.Models.BaseModels
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
