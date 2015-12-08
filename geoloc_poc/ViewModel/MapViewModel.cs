using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Splat;
using System.Collections.Generic;

namespace geoloc_poc
{

	public class LocationsFoundsEventArgs
	{
		public IEnumerable<ILocation> Locations { get ; private set ;}

		public LocationsFoundsEventArgs (IEnumerable<ILocation> locations)
		{
			Locations = locations;
		}
	}

	public class MapViewModel
	{
		public ILocation UserLocation { get ; private set ; }

		public event EventHandler<LocationsFoundsEventArgs> DidFinishRetrievingUserLocation = delegate {};
		public event EventHandler<LocationsFoundsEventArgs> DidFinishRetrievingNearbyLocations = delegate {};


		ILocationManager locationManager { get { 
				return Locator.Current.GetService<ILocationManager> ();
			}
		}


		ILocationFinder locationFinder { get { 
				return Locator.Current.GetService<ILocationFinder> ();
			}
		}

		#region Commands


		public RelayCommand _tryLocatingUser;
		public RelayCommand TryLocatingUser { 
			get { 
				if (_tryLocatingUser == null) {
					_tryLocatingUser = new RelayCommand (() => locationManager.RetrieveUserLocation ().ContinueWith (t => {

						this.UserLocation =  t.Result;

						var eventArgs = new LocationsFoundsEventArgs (new[] {
							this.UserLocation
						});

						DidFinishRetrievingUserLocation (this, eventArgs);
					}, TaskScheduler.FromCurrentSynchronizationContext ()));

				}
				return _tryLocatingUser;
			}
		}

		public RelayCommand _findNearby;
		public RelayCommand FindNearby { 
			get { 

				if (_findNearby == null) {
					_findNearby = new RelayCommand (() => locationFinder.FindClosestPoint (this.UserLocation).ContinueWith (t => {

						var eventArgs = new LocationsFoundsEventArgs (t.Result);

						DidFinishRetrievingNearbyLocations(this, eventArgs);

					}, TaskScheduler.FromCurrentSynchronizationContext ()));
				}

				return _findNearby;
			}
		}

		#endregion
	}
}

