namespace Victim.Service.Api.Models
{
    public class Emergency
    {
        public Guid Id { get; set; }

        public string? Type { get; set; } // "Medical", "Fire", etc.

        public string? Description { get; set; }

        public string? Severity { get; set; } // "Low", "Medium", "High", "Critical"

        public DateTime ReportedAt { get; set; }

        public string? Status { get; set; } // "Reported", "Dispatched", etc.

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<Victim> Victims { get; set; } = new();
    }
}
