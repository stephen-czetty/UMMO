using System;
using System.Diagnostics.CodeAnalysis;

namespace UMMO.TestingUtils.RandomData {
    public abstract class FluentlyResemblingA<T>
    {
        [Obsolete("Use ResemblingA instead")]
        [ExcludeFromCodeCoverage]
        public FluentlyResemblingA<T> Resembling => this;

        [Obsolete("Use ResemblingA instead")]
        [ExcludeFromCodeCoverage]
        public T A => ResemblingA;

        public abstract T ResemblingA { get; }

        public abstract string Value { get; }

        public static implicit operator string(FluentlyResemblingA<T> resembling)
        {
            return resembling.Value;
        }
    }
}