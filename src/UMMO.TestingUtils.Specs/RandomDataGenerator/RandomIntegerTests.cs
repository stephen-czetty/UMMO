using System;
using FluentAssertions;
using Xunit;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator {
    public class RandomIntegerTests : RandomDataGeneratorTestsBase
    {
        [Fact]
        public void When_getting_random_integer()
        {
            var expectedInteger = RealRandom.Next();
            Random.Setup(r => r.Next()).Returns(expectedInteger);
            var randomInteger = Subject.Integer;

            randomInteger.Value.Should().Be(expectedInteger);
            int value = randomInteger;
            value.Should().Be(expectedInteger);
        }

        [Fact]
        public void When_getting_random_integer_between_two_values()
        {
            int minValue = A.Random.Integer;
            int maxValue = A.Random.Integer.GreaterThan(minValue + 1);

            var value = A.Random.Integer.Between(minValue, maxValue);

            value.Should().BeGreaterOrEqualTo(minValue);
            value.Should().BeLessOrEqualTo(maxValue);
        }

        [Fact]
        public void When_getting_random_integer_between_two_values_in_incorrect_order()
        {
            int minValue = A.Random.Integer;
            int maxValue = A.Random.Integer.LessThan(minValue);

            Func<int> call = () => A.Random.Integer.Between(minValue, maxValue);

            call.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void When_getting_random_integer_between_two_equal_values()
        {
            int expectedValue = A.Random.Integer;
            int actualValue = A.Random.Integer.Between(expectedValue, expectedValue);

            actualValue.Should().Be(expectedValue);
        }

        [Fact]
        public void When_getting_random_integer_greater_than_value()
        {
            int minValue = A.Random.Integer.LessThan((int)(int.MaxValue * 0.95));

            var value = A.Random.Integer.GreaterThan(minValue);

            value.Should().BeGreaterOrEqualTo(minValue);
        }

        [Fact]
        public void When_getting_random_integer_less_than_value()
        {
            int maxValue = A.Random.Integer.GreaterThan((int)(int.MinValue * 0.95));

            var value = A.Random.Integer.LessThan(maxValue);

            value.Should().BeLessOrEqualTo(maxValue);
        }
    }
}