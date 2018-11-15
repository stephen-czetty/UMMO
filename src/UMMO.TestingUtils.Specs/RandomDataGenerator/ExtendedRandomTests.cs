using System;
using FluentAssertions;
using Xunit;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    public class ExtendedRandomTests
    {
        private readonly RandomData.ExtendedRandom _randomData;

        public ExtendedRandomTests()
        {
            _randomData = new RandomData.ExtendedRandom();
        }

        [Fact]
        public void When_getting_next_bytes__should_return_valid_data()
        {
            var byteCount = A.Random.Integer.Between(1, 100);
            var bytes = _randomData.NextBytes(byteCount);

            bytes.Should().NotBeNullOrEmpty();
            bytes.Length.Should().Be(byteCount);
        }

        [Fact]
        public void When_getting_zero_bytes__should_return_empty_array()
        {
            var bytes = _randomData.NextBytes(0);

            bytes.Should().NotBeNull();
            bytes.Length.Should().Be(0);
        }

        [Fact]
        public void When_getting_next_decimal_between_values_where_min_is_greater_than_max__should_throw_exception()
        {
            Func<decimal> action = () => _randomData.NextDecimal(1, 0);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_getting_next_decimal_with_maxvalue__should_return_value_less_than_or_equal_to_max()
        {
            var value = _randomData.NextDecimal(1);

            value.Should().BeLessOrEqualTo(1);
        }

        [Fact]
        public void When_getting_next_decimal_with_min_and_max_values__should_return_value_in_range()
        {
            var value = _randomData.NextDecimal(-1, 1);

            value.Should().BeGreaterOrEqualTo(-1);
            value.Should().BeLessOrEqualTo(1);
        }

        [Fact]
        public void When_getting_next_decimal_in_range_near_to_maxvalue__should_not_overflow()
        {
            var value = _randomData.NextDecimal(decimal.MaxValue - 1, decimal.MaxValue);

            value.Should().BeGreaterOrEqualTo(decimal.MaxValue - 1);
            value.Should().BeLessOrEqualTo(decimal.MaxValue);
        }

        [Fact]
        public void When_getting_next_decimal_in_range_near_to_minvalue__should_not_overflow()
        {
            var value = _randomData.NextDecimal(decimal.MinValue, decimal.MinValue + 1);

            value.Should().BeGreaterOrEqualTo(decimal.MinValue);
            value.Should().BeLessOrEqualTo(decimal.MinValue + 1);
        }

        [Fact]
        public void When_getting_next_decimal_with_identical_min_and_max_values__should_return_that_value()
        {
            decimal expected = A.Random.Decimal;
            var value = _randomData.NextDecimal(expected, expected);

            value.Should().Be(expected);
        }

        [Fact]
        public void When_getting_next_long_between_values_where_min_is_greater_than_max__should_throw_exception()
        {
            Func<long> action = () => _randomData.NextLong(1, 0);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_getting_next_long_with_maxvalue__should_return_value_less_than_or_equal_to_max()
        {
            var value = _randomData.NextLong(1);

            value.Should().BeLessOrEqualTo(1);
        }

        [Fact]
        public void When_getting_next_long_with_min_and_max_values__should_return_value_in_range()
        {
            var value = _randomData.NextLong(-1, 1);

            value.Should().BeLessOrEqualTo(1);
            value.Should().BeGreaterOrEqualTo(-1);
        }

        [Fact]
        public void When_getting_next_long_in_range_near_to_maxvalue__should_not_overflow()
        {
            var value = _randomData.NextLong(long.MaxValue - 1, long.MaxValue);

            value.Should().BeLessOrEqualTo(long.MaxValue);
            value.Should().BeGreaterOrEqualTo(long.MaxValue - 1);
        }

        [Fact]
        public void When_getting_next_long_in_range_near_to_minvalue__should_not_overflow()
        {
            var value = _randomData.NextLong(long.MinValue, long.MinValue + 1);

            value.Should().BeGreaterOrEqualTo(long.MinValue);
            value.Should().BeLessOrEqualTo(long.MinValue + 1);
        }

        [Fact]
        public void When_getting_next_long_with_identical_min_and_max_values__should_return_that_value()
        {
            long expected = A.Random.LongInteger;
            var value = _randomData.NextLong(expected, expected);

            value.Should().Be(expected);
        }
    }
}