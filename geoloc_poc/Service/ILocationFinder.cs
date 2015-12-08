using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace geoloc_poc
{

	/// <summary>
	/// Class for retrieving POI aroud a given location.
	/// Might be the role of the webservice.
	/// </summary>
	public interface ILocationFinder
	{

		Task<IEnumerable<ILocation>> FindClosestPoint(ILocation myLocation);
	}
}

