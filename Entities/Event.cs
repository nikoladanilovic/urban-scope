using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
