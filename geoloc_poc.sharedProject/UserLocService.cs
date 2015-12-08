using System;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using Splat;

namespace geoloc_poc.sharedProject
{
	public class UserLocService : ILocationManager
	{

		ILocationFactory LocationBuilder { get { 
				return Locator.Current.GetService<ILocationFactory> ();
			}
		}

		object Context = null;

		public UserLocService (object context = null)
		{
			Context = context;
		}

		#region ILocationManager implementation

		Geolocator CreateLocator ()
		{
			#if __ANDROID__

			return new Geolocator(this.Context as Android.Content.Context){ DesiredAccuracy = 50};

			#elif __IOS__

			return new Geolocator(){ DesiredAccuracy = 50};

			#endif

			return null;
		}

		public async Task<ILocation> RetrieveUserLocation ()
		{

			var geolocator = CreateLocator();

			var result = await geolocator.GetPositionAsync (timeout: 10000);

			return LocationBuilder.CreateUserLocation(result.Longitude, result.Latitude);

		}

		#endregion
	}
}

