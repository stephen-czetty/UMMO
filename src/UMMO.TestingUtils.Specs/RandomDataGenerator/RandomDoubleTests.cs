using System;
using FluentAssertions;
using Xunit;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator {
    public class RandomDoubleTests : RandomDataGeneratorTestsBase
    {
        [Fact]
        public void When_getting_random_double()
        {
            var expectedDouble = RealRandom.NextDouble();
            Random.Setup(r => r.NextDouble()).Returns(expectedDouble);
            Random.Setup(r => r.Next()).Returns(1);
            var randomDouble = Subject.Double;

            randomDouble.Value.Should().Be(expectedDouble);
            double value = randomDouble;
            value.Should().Be(expectedDouble);
        }

        [Fact]
        public void When_getting_random_double_between_two_values_in_incorrect_order()
        {
            double minValue = A.Random.Double;
            double maxValue = A.Random.Double.LessThan(minValue - 1);

            Func<double> call = () => A.Random.Double.Between(minValue, maxValue);

            call.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void When_getting_random_double_between_two_values()
        {
            double minValue = A.Random.Double;
            double maxValue = A.Random.Double.GreaterThan(minValue + 1);

            var value = A.Random.Double.Between(minValue, maxValue);

            value.Should().BeGreaterOrEqualTo(minValue);
            value.Should().BeLessOrEqualTo(maxValue);
        }

        [Fact]
        public void When_getting_random_double_between_two_equal_values()
        {
            double expectedValue = A.Random.Double;

            var actualValue = A.Random.Double.Between(expectedValue, expectedValue);

            actualValue.Should().Be(expectedValue);
        }

        [Fact]
        public void When_getting_random_double_near_max_value()
        {
            double minValue = double.MaxValue * 0.95;
            double value = A.Random.Double.Between(minValue, double.MaxValue);

            value.Should().BeGreaterOrEqualTo(minValue);
        }

        [Fact]
        public void When_getting_random_double_near_min_value()
        {
            double maxValue = double.MinValue * 0.95;
            double value = A.Random.Double.Between(double.MinValue, maxValue);

            value.Should().BeLessOrEqualTo(maxValue);
        }
    }
}