using System;
using FluentAssertions;
using Xunit;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator {
    public class RandomLongTests : RandomDataGeneratorTestsBase
    {
        [Fact]
        public void When_getting_random_long()
        {
            var expectedLong = RealRandom.NextLong();
            Random.Setup(r => r.NextLong()).Returns(expectedLong);
            var randomLong = Subject.LongInteger;

            randomLong.Value.Should().Be(expectedLong);
            long value = randomLong;
            value.Should().Be(expectedLong);
        }

        [Fact]
        public void When_getting_random_long_between_two_values()
        {
            long minValue = A.Random.LongInteger;
            long maxValue = A.Random.LongInteger.GreaterThan(minValue + 1);

            var value = A.Random.LongInteger.Between(minValue, maxValue);

            value.Should().BeGreaterOrEqualTo(minValue);
            value.Should().BeLessOrEqualTo(maxValue);
        }

        [Fact]
        public void When_getting_random_long_between_two_values_in_incorrect_order()
        {
            long minValue = A.Random.LongInteger;
            long maxValue = A.Random.LongInteger.LessThan(minValue);

            Func<long> call = () => A.Random.LongInteger.Between(minValue, maxValue);

            call.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void When_getting_random_long_between_two_equal_values()
        {
            long expectedValue = A.Random.LongInteger;
            long actualValue = A.Random.LongInteger.Between(expectedValue, expectedValue);

            actualValue.Should().Be(expectedValue);
        }

        [Fact]
        public void When_getting_random_long_greater_than_value()
        {
            long minValue = A.Random.LongInteger.LessThan((long)(long.MaxValue * 0.95));

            var value = A.Random.LongInteger.GreaterThan(minValue);

            value.Should().BeGreaterOrEqualTo(minValue);
        }

        [Fact]
        public void When_getting_random_long_less_than_value()
        {
            long maxValue = A.Random.LongInteger.GreaterThan((long)(long.MinValue * 0.95));

            var value = A.Random.LongInteger.LessThan(maxValue);

            value.Should().BeLessOrEqualTo(maxValue);
        }
    }
}