using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class CreateMarketplaceItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
