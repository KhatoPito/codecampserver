using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers.Extensions
{
	public static class DateTimeExtensions
	{
		public static IEnumerable<SelectListItem> GetNoonSelectItems(this DateTime dateTime)
		{
			TimeSpan time = dateTime.TimeOfDay;

			yield return new SelectListItem()
			{
				Selected = time.Hours < 13,
				Text = "A.M.",
				Value = "0"
			};
			yield return new SelectListItem()
			{
				Selected = time.Hours >= 13,
				Text = "P.M.",
				Value = "12"
			};
		}

		public static IEnumerable<SelectListItem> GetHourSelectItems(this DateTime datetime)
		{
			int hour = datetime.Hour;
			if (hour == 0)
			{
				hour = 12;
			}
			if (hour > 12)
			{
				hour -= 12;
			}
			for (int i = 1; i <= 12; i++)
			{
				yield return new SelectListItem()
				{
					Selected = i == hour,
					Text = i.ToString(),
					Value = i.ToString()
				};
			}
		}
		public static IEnumerable<SelectListItem> GetMinuteSelectItems(this DateTime datetime)
		{
			int minute = datetime.Minute;

			for (int i = 0; i < 60; i = i + 15)
			{
				yield return new SelectListItem()
				{
					Selected = i == minute,
					Text = i.ToString(),
					Value = i.ToString()
				};
			}
		}

	}
}