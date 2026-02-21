using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class UpdateMarketplaceItemDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
