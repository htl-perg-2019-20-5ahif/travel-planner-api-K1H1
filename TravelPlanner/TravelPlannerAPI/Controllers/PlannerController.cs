using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelPlannerLibrary;

namespace TravelPlannerAPI.Controllers
{
	[ApiController]
	[Route("api/travelplan")]
	public class PlannerController : ControllerBase
	{

		private HttpClient client;
		private string filename = "travelPlan.json";


		public PlannerController(IHttpClientFactory factory)
		{
			this.client = factory.CreateClient();
			this.client.BaseAddress = new Uri("https://cddataexchange.blob.core.windows.net/data-exchange/htl-homework/");

		}

		[HttpGet]
		public async Task<IActionResult> GetTrips([FromQuery] string from, [FromQuery] string to, [FromQuery]string start)
		{
			HttpResponseMessage response = await client.GetAsync(filename);
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			Route[] routes = JsonSerializer.Deserialize<Route[]>(responseBody);

			//Console.WriteLine("routes: " + routes[1].City);
			//Console.WriteLine("response: " + responseBody);

			var planner = new PlannerLibrary(routes);
			var trip = planner.GetConnection(from, to, start);

			if (trip != null)
			{
				return Ok(trip);

			}
			else
			{
				return NotFound("No trip found");
			}

		}
	}
}
