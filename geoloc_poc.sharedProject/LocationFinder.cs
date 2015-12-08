using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Splat;

namespace geoloc_poc.sharedProject
{
	public class LocationFinder : ILocationFinder
	{

		ILocationFactory LocationBuilder { get { 
				return Locator.Current.GetService<ILocationFactory> ();
			}
		}

		public LocationFinder ()
		{
		}

		#region ILocationFinder implementation

		public async  Task<IEnumerable<ILocation>> FindClosestPoint (ILocation myLocation)
		{
			Random r = new Random ();

			await Task.FromResult (true);

			var list = new List<ILocation> ();

			for (int i = 0; i < r.Next (10); i++) {

				Double longitude = r.NextDouble () - .5 + myLocation.Longitude;

				Double latitude = r.NextDouble () - .5 + myLocation.Latitude;


				var loc = LocationBuilder.CreateLocation (longitude, latitude, string.Format ("POI_{0}", i));

				list.Add (loc);

			}

			return list;

		}

		#endregion
	}
}

