using System;
using Splat;


namespace geoloc_poc.sharedProject
{
	public static class LocatorLoader
	{

		public static void Load(object context = null)
		{
			Locator.CurrentMutable.RegisterLazySingleton (() => new LocationFactory (), typeof(ILocationFactory));

			Locator.CurrentMutable.RegisterLazySingleton (() => new LocationFinder (), typeof(ILocationFinder));

			Locator.CurrentMutable.RegisterLazySingleton (() => new UserLocService (context), typeof(ILocationManager));
		}
	}
}

