﻿using System;
using CodeCampServer.Core.Domain.Model.Enumerations;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CodeCampServer.Infrastructure.DataAccess.Mappings
{
	public class EnumerationTypeConvention : IPropertyConvention, IPropertyConventionAcceptance
	{
		private static readonly Type _openType = typeof(EnumerationType<>);

		public void Apply(IPropertyInstance instance)
		{
			Type closedType = _openType.MakeGenericType(instance.Property.PropertyType);

			instance.CustomType(closedType);
		}

		public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
		{
			criteria.Expect(x => typeof(Enumeration).IsAssignableFrom(x.Property.PropertyType));
		}
	}
}