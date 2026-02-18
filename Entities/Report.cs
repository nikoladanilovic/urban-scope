using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Comment> Comments { get; set; } = new();
    }
}
