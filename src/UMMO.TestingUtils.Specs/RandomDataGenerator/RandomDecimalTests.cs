using System;
using FluentAssertions;
using Xunit;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator {
    public class RandomDecimalTests : RandomDataGeneratorTestsBase
    {
        [Fact]
        public void When_getting_random_decimal()
        {
            var expectedDecimal = RealRandom.NextDecimal();
            Random.Setup(r => r.NextDecimal()).Returns(expectedDecimal);
            var randomDecimal = Subject.Decimal;

            randomDecimal.Value.Should().Be(expectedDecimal);
            decimal value = randomDecimal;
            value.Should().Be(expectedDecimal);
        }

        [Fact]
        public void When_getting_random_decimal_between_two_equal_values()
        {
            decimal value = A.Random.Decimal;
            var randomDecimal = A.Random.Decimal.Between(value, value);

            randomDecimal.Should().Be(value);
        }

        [Fact]
        public void When_getting_random_decimal_between_two_values()
        {
            decimal minValue = A.Random.Decimal;
            var maxValue = A.Random.Decimal.GreaterThan(minValue);

            var randomDecimal = A.Random.Decimal.Between(minValue, maxValue);

            randomDecimal.Should().BeGreaterOrEqualTo(minValue);
            randomDecimal.Should().BeLessOrEqualTo(maxValue);
        }

        [Fact]
        public void When_getting_random_decimal_between_two_value_in_incorrect_order()
        {
            decimal minValue = A.Random.Decimal;
            decimal maxValue = A.Random.Decimal.LessThan(minValue - 1);

            Func<decimal> call = () => A.Random.Decimal.Between(minValue, maxValue);

            call.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void When_getting_random_decimal_greater_than_negative_number()
        {
            decimal minValue = A.Random.Decimal.LessThan(-1);

            decimal actualValue = A.Random.Decimal.GreaterThan(minValue);

            actualValue.Should().BeGreaterOrEqualTo(minValue);
        }

        [Fact]
        public void When_getting_random_decimal_less_than_positive_number()
        {
            decimal maxValue = A.Random.Decimal.GreaterThan(1);

            decimal actualValue = A.Random.Decimal.LessThan(maxValue);

            actualValue.Should().BeLessOrEqualTo(maxValue);
        }
    }
}