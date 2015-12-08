using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace geoloc_poc
{
	public interface IMapViewModel
	{
		ILocation UserLocation { get ; }

		event EventHandler<LocationsFoundsEventArgs> DidFinishRetrievingUserLocation;
		event EventHandler<LocationsFoundsEventArgs> DidFinishRetrievingNearbyLocations;


		ICommand TryLocatingUser { get ; }

		ICommand FindNearby { get ;}

		string AddressText { get ; set ;}

		ObservableCollection<AutoCompleteSuggestion<ILocation>> Suggestions { get;}
	}




	public class LocationsFoundsEventArgs
	{
		public IEnumerable<ILocation> Locations { get ; private set ;}

		public LocationsFoundsEventArgs (IEnumerable<ILocation> locations)
		{
			Locations = locations;
		}
	}
}

