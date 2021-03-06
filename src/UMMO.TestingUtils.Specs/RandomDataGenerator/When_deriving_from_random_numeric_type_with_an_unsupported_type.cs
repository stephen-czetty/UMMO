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
            // Cannot completely cover this method, as the base class will throw an exception
            // before the constructor can exit.
#if NET461
            [ ExcludeFromCodeCoverage ]
#endif
            public BadRandomNumeric( IRandom random ) : base( random ) {}

            // For test coverage
#pragma warning disable 612,618
            internal BadRandomNumeric() {}
#pragma warning restore 612,618

            #region Overrides of RandomNumericType<BadNumeric>

            public override Boolean Value
            {
                get { return true; }
            }

            protected override Boolean GetBetween( Boolean minValue, Boolean maxValue )
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

    [ Subject( typeof(BadRandomNumeric) ) ]
    public class When_using_bad_random_numeric : BadRandomNumericTestsBase
    {
        private Establish Context = () => _badRandomNumeric = new BadRandomNumeric();

        private Because Of = () => _boolValue = _badRandomNumeric.Value;

        private It Should_return_boolean_when_calling_value
            =
            () => _boolValue.ShouldBeOfExactType< bool >();

        private It Should_throw_not_implemented_exception_when_calling_between
            =
            () =>
            Catch.Exception( () => _badRandomNumeric.Between( false, true ) ).ShouldBeOfExactType
                < NotImplementedException >();

        private static BadRandomNumeric _badRandomNumeric;
        private static bool _boolValue;
    }

    #endregion
}
