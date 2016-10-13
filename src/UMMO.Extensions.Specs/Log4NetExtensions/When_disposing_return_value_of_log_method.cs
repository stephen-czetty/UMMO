using System;
using log4net;
using log4net.Core;
using Machine.Fakes;
using Machine.Specifications;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    [Subject(typeof(Extensions.Log4NetExtensions))]
    public class When_disposing_return_value_of_log_method : Log4NetExtensionsSpecsBase
    {
        private Establish Context = () => ReturnValue = The<ILog>().LogMethod();

        private Because Of = () => ( (IDisposable)ReturnValue ).Dispose();

        private It Should_call_i_logger_log_when_disposed
            = () => The<ILogger>().WasToldTo(l => l.Log(Param<Type>.IsNotNull, Param<Level>.IsNotNull, Param<object>.IsNotNull,
                                Param<Exception>.IsNull) );
    }
}