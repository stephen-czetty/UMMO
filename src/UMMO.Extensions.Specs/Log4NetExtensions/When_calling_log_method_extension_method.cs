#if NET461
using System;
using log4net;
using log4net.Core;
using Machine.Fakes;
using Machine.Specifications;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    [Subject(typeof(Extensions.Log4NetExtensions))]
    public class When_calling_log_method_extension_method : Log4NetExtensionsSpecsBase
    {
        private Because Of = () => ReturnValue = The<ILog>().LogMethod();

        private It Should_have_called_i_logger_log
            = () => The<ILogger>().WasToldTo(l => l.Log(Param<Type>.IsNotNull, Param<Level>.IsNotNull, Param<object>.IsNotNull,
                                Param<Exception>.IsNull));

        private It Should_return_an_i_exceptionlogger
            = () => ReturnValue.ShouldBeAssignableTo< ILogWrapper >();
    }
}
#endif
