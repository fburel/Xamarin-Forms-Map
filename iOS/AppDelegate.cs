using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using geoloc_poc.sharedProject;

namespace geoloc_poc.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			Xamarin.FormsMaps.Init();

			LocatorLoader.Load ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

