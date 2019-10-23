using System;
using System.Collections.Generic;


namespace TravelPlannerLibrary
{
	public class PlannerLibrary
	{

		Route[] routes;
		public PlannerLibrary(Route[] routes)
		{
			this.routes = routes;
		}

		public Trip GetConnection(string from, string to, string start)
		{


			// basic requirements
			if (from == "Linz")
			{
				return LoopRoutes(to, from, start);
			}
			else
			if (to == "Linz")
			{
				return LoopRoutes(from, to, start);

			}

			//bonus requirements (first to Linz and then to the destination city)
			var toLinz = LoopRoutes(from, "Linz", start);
			var fromLinz = LoopRoutes(to, "Linz", toLinz.Arrive);

			return new Trip()
			{
				fromCity = from,
				toCity = to,
				Leave = toLinz.Leave,
				Arrive = fromLinz.Arrive
			};

		}
	

		public Trip LoopRoutes(string from, string to, string start)
		{
			foreach (Route r in routes)
			{
				if (r.City == from)
				{
					foreach (TimeStamp ts in r.ToLinz)
					{
						if (ts.Leave.CompareTo(start) >= 0)
						{
							return new Trip()
							{
								fromCity = from,
								toCity = to,
								Leave = ts.Leave,
								Arrive = ts.Arrive
							};
						}
					}

				}

			}
			return null;
		}
	}
}
