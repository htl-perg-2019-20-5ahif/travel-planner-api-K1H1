using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlannerLibrary
{
	public class Route
	{
		public string City { get; set; }
		public TimeStamp[] ToLinz { get; set; }
		public TimeStamp[] FromLinz { get; set; }

	}

	public class TimeStamp
	{
		public string Arrive { get; set; }
		public string Leave { get; set; }
	}

	public class Trip : TimeStamp
	{
		public string fromCity { get; set; }
		public string toCity { get; set; }
	}
}
