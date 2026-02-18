using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class EventUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
