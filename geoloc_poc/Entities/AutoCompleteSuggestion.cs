
using System;

namespace geoloc_poc
{
	public class AutoCompleteSuggestion<T>
	{

		public T Value { get ; private set ;}

		public String DisplayedName { get ; private set ;}

		public AutoCompleteSuggestion (T value, string displayedText)
		{
			this.DisplayedName = displayedText;
			this.Value = value;
		}
	}
}

