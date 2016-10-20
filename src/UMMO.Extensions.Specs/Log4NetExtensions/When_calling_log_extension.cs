#if NET461
using System;
using log4net;
using log4net.Core;
using Machine.Fakes;
using Machine.Specifications;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    [Subject(typeof(Extensions.Log4NetExtensions))]
    public class When_calling_log_extension : Log4NetExtensionsSpecsBase
    {
        private Establish Context = () =>
                                        {
                                            _logger = The<ILog>().LogMethod();
                                        };

        private Because Of = () => _logger.LogException(new Exception());

        private It Should_call_i_logger_log_when_called
            =
            () =>
                The< ILogger >()
                    .WasToldTo(
                        l =>
                            l.Log( Param< Type >.IsNotNull, Param< Level >.IsNotNull, Param< object >.IsNotNull,
                                Param< Exception >.IsNotNull ) );

        private static ILogWrapper _logger;
    }
}
#endif
