// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Mapping.Types.Core.Range.IntegerRange
{
	public class IntegerRangeTest
	{
		[IntegerRange]
		public Nest.IntegerRange Range { get; set; }
	}

	[SkipVersion("<5.2.0", "dedicated range types is a new 5.2.0 feature")]
	public class IntegerRangeAttributeTests : AttributeTestsBase<IntegerRangeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				range = new
				{
					type = "integer_range"
				}
			}
		};
	}
}
