using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using UMMO.TestingUtils.RandomData;
using Xunit;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    public class BadRandomNumericTests
    {
        [Fact]
        public void Instantiating_random_numeric_type_with_type_that_has_no_max_or_min_value()
        {
            Func<RandomNumericType<bool>> call = () => new BadRandomNumeric(null);

            call.Should().Throw<RandomDataException>();
        }

        [ExcludeFromCodeCoverage]
        private class BadRandomNumeric : RandomNumericType<bool>
        {
            public BadRandomNumeric(IRandom random) : base(random) { }


            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public override bool Value { get; }

            protected override bool GetBetween(bool minValue, bool maxValue)
            {
                throw new NotImplementedException();
            }

            protected override bool Increment(bool value)
            {
                throw new NotImplementedException();
            }

            protected override bool Decrement(bool value)
            {
                throw new NotImplementedException();
            }
        }
    }
}
