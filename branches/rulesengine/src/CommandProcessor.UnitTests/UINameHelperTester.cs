using System;
using System.Linq.Expressions;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.RulesEngine;

namespace Tarantino.UnitTests.Core.Common
{
	[TestFixture]
	public class UINameHelperTester
	{
		public class SomeForm
		{
			public object GetSomeValue()
			{
				return null;
			}
		}

		[Test]
		public void Should_build_name_from_basic_expression()
		{
			Expression<Func<DrugTestForm, object>> expression = f => f.DrugTestId;
			UINameHelper.BuildNameFrom(expression).ShouldEqual("DrugTestId");
		}

		[Test]
		public void Should_build_name_from_indexed_expression()
		{
			Expression<Func<DrugTestForm, object>> expression = f => f.DrugTestDrugTestResults[5].SubstanceTested;
			UINameHelper.BuildNameFrom(expression).ShouldEqual("DrugTestDrugTestResults[5].SubstanceTested");
		}

		[Test]
		public void should_build_name_from_typed_expression()
		{
			UINameHelper.BuildNameFrom<DrugTestForm>(f => f.DrugTestId).ShouldEqual("DrugTestId");
		}

		//[Test]
		//public void Should_build_name_for_enumerated_input()
		//{
		//    Expression<Func<AbuseForm, object>> expression = f => f.AbusePhysical;
		//    UINameHelper.BuildIdFrom(expression, YesNo.Yes).ShouldEqual("AbusePhysical_1");
		//}

		[Test]
		public void Should_extract_index_values_from_expressions()
		{
			Expression<Func<DrugTestForm, object>> expression1 = f => f.DrugTestId;
			Expression<Func<DrugTestForm, object>> expression2 = f => f.DrugTestDrugTestResults[5].SubstanceTested;

			UINameHelper.ExtractIndexValueFrom(expression1).ShouldBeNull();
			UINameHelper.ExtractIndexValueFrom(expression2).ShouldEqual(5);
		}

		[Test]
		public void Should_extract_label_values_from_expressions()
		{
			Expression<Func<DrugTestForm, object>> expression1 = f => f.DrugTestWitnessedById;
			Expression<Func<DrugTestForm, object>> expression2 = f => f.DrugTestDrugTestResults[5].SubstanceTested;
			Expression<Func<SomeForm, object>> expression3 = f => f.GetSomeValue();

			UINameHelper.BuildLabelFrom(expression1).ShouldEqual("Witnessed By");
			UINameHelper.BuildLabelFrom(expression2).ShouldEqual("Substance Tested");
			UINameHelper.BuildLabelFrom(expression3).ShouldEqual("Some Value");
		}
	}

	public class AbuseForm
	{
		public object AbusePhysical { get; set; }
	}

	public class DrugTestForm
	{
		public object DrugTestId { get; set; }

		public Foo[] DrugTestDrugTestResults { get; set; }

		[Label("Witnessed By")]
		public object DrugTestWitnessedById { get; set; }
	}

	public class Foo
	{
		public object SubstanceTested { get; set; }
	}
}