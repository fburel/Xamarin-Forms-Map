using System;

namespace geoloc_poc
{
	public interface ILocation
	{
		Double Longitude { get; }
		Double Latitude { get; }
		String Label { get; }
	}

	public interface ILocationFactory 
	{
		ILocation CreateUserLocation(Double lon, Double lat);
		ILocation CreateLocation(Double lon, Double lat, String name);
	}

	public class LocationFactory : ILocationFactory
	{
		private class Location : ILocation
		{
			public Double Longitude { get; private set ;}
			public Double Latitude { get; private set ;}
			public String Label { get; private set ;}

			public Location (Double lat, Double lon, String name)
			{
				this.Latitude = lat; 
				this.Longitude = lon;
				this.Label = name;
			}

		}


		#region ILocationFactory implementation

		public ILocation CreateUserLocation (double lon, double lat)
		{
			return new Location (lat, lon, "me");
		}
		public ILocation CreateLocation (double lon, double lat, string name)
		{
			return new Location (lat, lon, name);
		}

		#endregion
	}
}

