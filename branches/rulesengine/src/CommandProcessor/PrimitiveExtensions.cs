using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tarantino.RulesEngine
{
	public static class PrimitiveExtensions
	{
		public static string ParseKeyFromBrackets(this string original)
		{
			string cleanCode = original;

			if (cleanCode == null)
				return string.Empty;

			if (cleanCode.Contains("["))
			{
				int indexOfLeftBracket = cleanCode.IndexOf("[") + 1;
				cleanCode = cleanCode.Substring(indexOfLeftBracket, cleanCode.Length - indexOfLeftBracket);
			}

			if (cleanCode.Contains("]"))
			{
				int indexOfRightBracket = cleanCode.IndexOf("]");
				cleanCode = cleanCode.Substring(0, indexOfRightBracket);
			}

			return cleanCode;
		}

		public static bool IsEmpty(this Guid value)
		{
			return value == Guid.Empty;
		}

		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}


		public static T[] EmptyToNull<T>(this T[] array)
		{
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array;
		}

		public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable ?? Enumerable.Empty<T>();
		}

		public static List<T> EmptyToNull<T>(this List<T> list)
		{
			if (list == null || list.Count == 0)
			{
				return null;
			}
			return list;
		}

		public static string ToXHTMLLink(this string url)
		{
			return !string.IsNullOrEmpty(url) ? url.Replace("&", "&amp;") : string.Empty;
		}

		public static string ToStandardDateWithTime(this DateTime? value, string valueIfNull)
		{
			if (value == null)
				return String.Empty;

			return value.Value.ToString("MM/dd/yyyy HH:mm");
		}

		public static string ToStandardDateWithTime(this DateTime value)
		{
			return value.ToString("MM/dd/yyyy HH:mm");
		}

		public static string ToStandardDate(this DateTime value)
		{
			return value.ToString("MM/dd/yyyy");
		}

		public static string ToStandardDate(this DateTime? value)
		{
			return value.HasValue ? value.Value.ToString("MM/dd/yyyy") : "";
		}

		public static string ToStandardTime(this DateTime value)
		{
			return value.ToString("HH:mm");
		}

		public static string ToStandardDate(this DateTime? value, string valueIfNull)
		{
			if (value.HasValue)
				return ToStandardDate(value.Value);
			return valueIfNull;
		}

		public static string ToFormattedString(this TimeSpan value)
		{
			var list = new List<string>(3);

			int days = value.Days;
			int hours = value.Hours;
			int minutes = value.Minutes;

			if (days > 1)
				list.Add(String.Format("{0} days", days));
			else if (days == 1)
				list.Add(String.Format("{0} day", days));

			if (hours > 1)
				list.Add(String.Format("{0} hours", hours));
			else if (hours == 1)
				list.Add(String.Format("{0} hour", hours));

			if (minutes > 1)
				list.Add(String.Format("{0} minutes", minutes));
			else if (minutes == 1)
				list.Add(String.Format("{0} minute", minutes));

			return String.Join(", ", list.ToArray());
		}

		public static string ToNullSafeString(this object value)
		{
			return value == null ? String.Empty : value.ToString();
		}

		public static string ToLowerCamelCase(this string value)
		{
			return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
		}

		public static string ToUpperCamelCase(this string value)
		{
			return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
		}

		public static string ToSeparatedWords(this string value)
		{
			return Regex.Replace(value, "([A-Z][a-z]?)", " $1").Trim();
		}

		public static string WrapEachWith<T>(this IEnumerable<T> values, string before, string after, string separator)
		{
			return string.Join(separator, values.Select(x => string.Format("{0}{1}{2}", before, x, after)).ToArray());
		}

		public static Dictionary<string, object> ToDictionary(this NameValueCollection nameValueCollection)
		{
			var dictionary = new Dictionary<string, object>();
			foreach (string key in nameValueCollection)
			{
				dictionary.Add(key, nameValueCollection[key]);
			}
			return dictionary;
		}

		public static DateTime? ToNullableDate(this string value)
		{
			DateTime? dateTime = Converter.ToDateTime(value, "0", "0");
			return dateTime;
		}

		public static DateTime? ToNullableDate(this string value, string hour, string minute)
		{
			DateTime? dateTime = Converter.ToDateTime(value, hour, minute);
			return dateTime;
		}

		public static string Capitalize(this string word)
		{
			return word.Substring(0, 1).ToUpperInvariant() + word.Substring(1);
		}
	}
}