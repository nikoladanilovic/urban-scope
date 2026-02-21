using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class MarketplaceItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
