using System;
using System.Threading.Tasks;

namespace geoloc_poc
{
	public interface ILocationManager
	{

		Task<ILocation> RetrieveUserLocation();

	}
}

