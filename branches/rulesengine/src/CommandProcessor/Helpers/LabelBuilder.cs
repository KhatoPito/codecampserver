using System.Collections;
using System.Reflection;

namespace Tarantino.RulesEngine
{
	public static class LabelBuilder
	{
		public static string GetLabel(MemberInfo memberInfo)
		{
			if (memberInfo == null)
			{
				return string.Empty;
			}

			string label = memberInfo.Name.Replace("Get", string.Empty).ToSeparatedWords();

			if (memberInfo.HasCustomAttribute<LabelAttribute>())
			{
				label = memberInfo.GetCustomAttribute<LabelAttribute>().Value;
			}

			return label;
		}

		public static string GetLabel(PropertyInfo property, string propertyName)
		{
			if (property.Name == propertyName)
				return GetLabel(property);

			if (property.PropertyType.IsArray || typeof (IList).IsAssignableFrom(property.PropertyType))
			{
				PropertyInfo info = property.PropertyType.GetElementType().GetProperty(propertyName);
				return GetLabel(info);
			}
			return GetLabel(property.PropertyType.GetProperty(propertyName));
		}
	}
}