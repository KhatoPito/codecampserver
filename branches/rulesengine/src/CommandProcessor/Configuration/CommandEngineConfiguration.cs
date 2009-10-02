using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tarantino.RulesEngine.Configuration
{
	public class CommandEngineConfiguration
	{
		private readonly IDictionary<Type, IMessageConfiguration> _messageConfigurations =
			new Dictionary<Type, IMessageConfiguration>();

		public IMessageConfiguration GetMessageConfiguration(Type messageType)
		{
			return _messageConfigurations[messageType];
		}

		public void Initialize(Assembly assembly)
		{
			IEnumerable<Type> messageDefinitionTypes =
				from t in assembly.GetTypes()
				where typeof (IMessageConfiguration).IsAssignableFrom(t) && !t.IsAbstract
				select t;

			foreach (Type messageDefinitionType in messageDefinitionTypes)
			{
				Type messageType = messageDefinitionType.BaseType.GetGenericArguments()[0];
				var messageConfiguration = (IMessageConfiguration) Activator.CreateInstance(messageDefinitionType);

				_messageConfigurations.Add(messageType, messageConfiguration);
			}
		}
	}
}