using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tarantino.RulesEngine
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (T item in items)
			{
				action(item);
			}
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
		{
			return items == null || items.Count() == 0;
		}

		public static bool IsNullOrEmpty(this IEnumerable items)
		{
			return items == null || !items.Cast<object>().Any();
		}

		public static IEnumerable<T> Do<T>(this IEnumerable<T> items, Action<T> action)
		{
			items.ForEach(action);
			return items;
		}


		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> items, TSource item)
		{
			return items.Except(new[] {item});
		}
	}
}