using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Maps;


namespace geoloc_poc
{
	public partial class MapPage : ContentPage
	{
		MapViewModel ViewModel = new MapViewModel();

		MapHelper helper = new MapHelper();

		public MapPage ()
		{
			BindingContext = ViewModel;

			InitializeComponent ();


			ViewModel.DidFinishRetrievingUserLocation += (sender, e) => {

				ILocation userLocation = e.Locations.FirstOrDefault();

				MyMap.MoveToRegion(helper.GetBestRegionForUserPosition(userLocation));




			};


			ViewModel.DidFinishRetrievingNearbyLocations += (sender, e) => {

				MyMap.MoveToRegion(helper.GetBestRegionShowingPoint(e.Locations, centeredAroundLocation:ViewModel.UserLocation));

				foreach (var item in e.Locations) {

					var position = new Position(item.Latitude, item.Longitude);

					var pin = new Pin {
						Type = PinType.Place,
						Position = position,
						Label = item.Label,
						Address = "custom detail info"
					};

					MyMap.Pins.Add(pin);

				}
			};


		}
	}
}

