namespace Victim.Service.Api.Models
{
    public class Victim
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }

        public string? Condition { get; set; }

        public string? ContactInfo { get; set; }

        public Guid EmergencyId { get; set; }
    }
}
