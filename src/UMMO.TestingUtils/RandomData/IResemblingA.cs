using System;

namespace UMMO.TestingUtils.RandomData
{
    public interface IResemblingA<out T>
    {
        T ResemblingA { get; }

        /// <summary>
        /// Syntactic sugar for fluent interface.  Returns instance.
        /// </summary>
        /// <value>This instance.</value>
        [Obsolete("Use ResemblingA instead")]
        T Resembling { get; }

        /// <summary>
        /// Syntactic sugar for fluent interface.  Returns instance.
        /// </summary>
        /// <value>This instance.</value>
        [Obsolete("Use ResemblingA instead")]
        T A { get; }
    }
}