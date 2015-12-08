using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Splat;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.Windows.Input;

namespace geoloc_poc
{

	public class MapViewModel : ViewModelBase, IMapViewModel
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



		#region Autocomplete

		string _addressText;
		public string AddressText { get { 
				return _addressText;
			} set { 
				
				Set (() => AddressText, ref _addressText, value);

				UpdateSuggestionList ();
		}}

		ObservableCollection<AutoCompleteSuggestion<ILocation>> _suggestions;
		public ObservableCollection<AutoCompleteSuggestion<ILocation>> Suggestions {
			get {
				if (_suggestions == null) {
					_suggestions = new ObservableCollection<AutoCompleteSuggestion<ILocation>> ();
				}
				return _suggestions;
			}
		}


		void UpdateSuggestionList ()
		{
//			if (this.AddressText.Length == 0) {
				LoadDefaultList ();
//			} else {
//				FetchListForText (this.AddressText);
//			}
		}

		void LoadDefaultList ()
		{
			this.Suggestions.Clear ();
			this.Suggestions.Add (new AutoCompleteSuggestion<ILocation> (null, "default 1"));
			this.Suggestions.Add (new AutoCompleteSuggestion<ILocation> (null, "default 2"));
			this.Suggestions.Add (new AutoCompleteSuggestion<ILocation> (null, "default 3"));
			this.Suggestions.Add (new AutoCompleteSuggestion<ILocation> (null, "default 4"));
		}

		void FetchListForText (string addressText)
		{
			throw new NotImplementedException ();
		}
		#endregion

		#region Commands


		public RelayCommand _tryLocatingUser;
		public ICommand TryLocatingUser { 
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
		public ICommand FindNearby { 
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

