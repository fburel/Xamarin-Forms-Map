using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using System.Linq;


namespace geoloc_poc
{
	public class MapHelper
	{

		public const int DefaultZoomLevelForCenteringAroundUser = 17; // 1 - 18


		public Xamarin.Forms.Maps.MapSpan GetBestRegionForUserPosition (ILocation userLocation)
		{
			var zoomlevel = DefaultZoomLevelForCenteringAroundUser;

			var latlongdegrees = 360 / (Math.Pow(2, zoomlevel));

			var position = new Xamarin.Forms.Maps.Position(userLocation.Latitude, userLocation.Longitude);

			return new MapSpan (position, latlongdegrees, latlongdegrees);
		}


		
		
		public Xamarin.Forms.Maps.MapSpan GetBestRegionShowingPoint (IEnumerable<ILocation> locations, ILocation centeredAroundLocation)
		{

			var userposition = new Xamarin.Forms.Maps.Position(centeredAroundLocation.Latitude, centeredAroundLocation.Longitude);

			var distances = from poi in locations select DistanceBetween(poi, centeredAroundLocation);

			var maxDistance = distances.Max (x => x.Miles);

			return MapSpan.FromCenterAndRadius (userposition, distances.First(x => Math.Abs (x.Miles - maxDistance) < .001));
		}


		Distance DistanceBetween (ILocation p1, ILocation p2)
		{

			var LongRadA 	= ToRadian (p1.Longitude);
			var LongRadB 	= ToRadian (p2.Longitude);
			var LatRadA 	= ToRadian (p1.Latitude);
			var LatRadB	= ToRadian (p2.Latitude);

			double dist = Math.Acos(Math.Sin(LatRadA)*Math.Sin(LatRadB) + Math.Cos(LatRadA)*Math.Cos(LatRadB)*Math.Cos(LongRadB-LongRadA));

			return Distance.FromMiles(ToDegrees(dist) * 60 * 1.1515);

		}

		double ToRadian(double deg)
		{
			return (deg * Math.PI / 180.0);
		}

		double ToDegrees(double radian)
		{
			return (radian / Math.PI * 180.0);
		}

		double Sin(double v)
		{
			return Math.Sin(v);
		}

		double Cos(double v)
		{
			return Math.Cos(v);
		}
	}
}

