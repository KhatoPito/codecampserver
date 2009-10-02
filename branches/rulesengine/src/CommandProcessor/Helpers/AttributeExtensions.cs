using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Tarantino.RulesEngine
{
	public static class AttributeExtensions
	{
		public static bool HasCustomAttribute<TAttribute>(this Type type) where TAttribute : Attribute
		{
			return TypeDescriptor.GetAttributes(type)[typeof (TAttribute)] != null;
		}

		public static bool HasCustomAttribute<T>(this MemberInfo p) where T : Attribute
		{
			return p.GetCustomAttributes(typeof (T), true).Any();
		}

		public static T[] GetCustomAttributes<T>(this MemberInfo p) where T : Attribute
		{
			return p.GetCustomAttributes(typeof (T), true).Cast<T>().ToArray();
		}


		public static T GetCustomAttribute<T>(this MemberInfo p) where T : Attribute
		{
			return GetCustomAttribute<T>(p, true);
		}

		public static T GetCustomAttribute<T>(this MemberInfo p, bool inherit) where T : Attribute
		{
			return p.GetCustomAttributes(typeof (T), inherit).Cast<T>().FirstOrDefault();
		}
	}
}