using System;
using log4net;
using log4net.Core;
using Machine.Fakes;
using Machine.Specifications;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    [Subject(typeof(Extensions.Log4NetExtensions))]
    public class When_throwing_exception_from_within_log_using_block : Log4NetExtensionsSpecsBase
    {
        private Because Of = () => _exception = Catch.Exception( TestClass.Test );

        private It Should_call_i_logger_log_when_exception_is_thrown
            = () => The<ILogger>().WasToldTo(l => l.Log(Param<Type>.IsNotNull, Param<Level>.IsNotNull, Param<object>.IsNotNull,
                                Param<Exception>.IsNull));

        private It Should_throw_an_exception
            = () => _exception.ShouldNotBeNull();

        private It Should_be_of_type_application_exception
            = () => _exception.ShouldBeOfExactType<ApplicationException>();

        private static Exception _exception;

        #region Test Class
        private static class TestClass
        {
            [CoverageExclude]
            public static void Test()
            {
                using (The<ILog>().LogMethod())
                {
                    throw new ApplicationException();
                }
            }
        }

        #endregion
    }
}