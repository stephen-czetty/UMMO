using System;
using System.Diagnostics.CodeAnalysis;
using Machine.Fakes;
using Machine.Specifications;
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    public class BadRandomNumericTestsBase : WithFakes {
        protected class BadRandomNumeric : RandomNumericType< bool >
        {
            public BadRandomNumeric( IRandom random ) : base( random ) {}

            #region Overrides of RandomNumericType<BadNumeric>

            [ExcludeFromCodeCoverage]
            public override bool Value => true;

            [ExcludeFromCodeCoverage]
            protected override bool GetBetween(bool minValue, bool maxValue )
            {
                throw new NotImplementedException();
            }

            #endregion
        }
    }

    [ Subject( typeof(RandomNumericType< >) ) ]
    public class Bad_random_numeric_tests : BadRandomNumericTestsBase
    {
        private Because Of = () => _exception = Catch.Exception( () => new BadRandomNumeric( An< IRandom >() ) );

        private It Should_throw_random_data_exception
            = () => _exception.ShouldBeOfExactType< RandomDataException >();

        private static Exception _exception;

        #region Test classes
    }

    #endregion
}
