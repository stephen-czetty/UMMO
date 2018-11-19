namespace UMMO.TestingUtils.RandomData
{
    public class RandomDouble : RandomNumericType<double>
    {
        public RandomDouble( IRandom random ) : base( random ) {}

        public override double Value => Random.NextDouble() * Random.Next();

        protected override double GetBetween( double minValue, double maxValue )
        {
            return ( Random.NextDouble() * ( maxValue - minValue ) ) + minValue;
        }

        protected override double Increment(double value)
        {
            return value + 1;
        }

        protected override double Decrement(double value)
        {
            return value - 1;
        }
    }
}