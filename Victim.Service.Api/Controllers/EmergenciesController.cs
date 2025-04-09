using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Victim.Service.Api.Models;

namespace Victim.Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergenciesController : ControllerBase
    {
        private readonly VictimDbContext _context;

        private readonly IHttpClientFactory _httpClient;

        public EmergenciesController(VictimDbContext context, IHttpClientFactory httpClient)

        {

            _context = context;

            _httpClient = httpClient;

        }
        [HttpPost]

        public async Task<ActionResult<Emergency>> ReportEmergency(Emergency emergency)

        {

            emergency.Id = Guid.NewGuid();

            emergency.ReportedAt = DateTime.UtcNow;

            emergency.Status = "Reported";

            // Get full address from LocationAPI

            var locationClient = _httpClient.CreateClient("LocationAPI");

            var addressResponse = await locationClient.GetAsync($"/api/location/address?lat={emergency.Latitude}&lng={emergency.Longitude}");

            if (addressResponse.IsSuccessStatusCode)

            {

                var address = await addressResponse.Content.ReadAsStringAsync();

                emergency.Description += $"\nLocation: {address}";

            }

            _context.Emergencies.Add(emergency);

            await _context.SaveChangesAsync();

            // Notify ResponderAPI

            var responderClient = _httpClient.CreateClient("ResponderAPI");

            await responderClient.PostAsJsonAsync("/api/responders/dispatch", new
            {

                EmergencyId = emergency.Id,

                Location = new { emergency.Latitude, emergency.Longitude },

                emergency.Type,

                emergency.Severity

            });

            return CreatedAtAction(nameof(GetEmergency), new { id = emergency.Id }, emergency);

        }
    }
}
