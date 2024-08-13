﻿namespace Contracts.Requests
{
    public class StoreUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Count { get; set; }
        
    }
}
