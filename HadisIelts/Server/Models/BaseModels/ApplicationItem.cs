﻿namespace HadisIelts.Server.Models.BaseModels
{
    public class ApplicationItem : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
    }
}
