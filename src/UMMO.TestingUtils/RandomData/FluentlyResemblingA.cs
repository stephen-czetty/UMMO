using System;

namespace UMMO.TestingUtils.RandomData {
    public abstract class FluentlyResemblingA<T>
    {
        [Obsolete("Use ResemblingA instead")]
        public FluentlyResemblingA<T> Resembling => this;

        [Obsolete("Use ResemblingA instead")]
        public T A => ResemblingA;

        public abstract T ResemblingA { get; }

        public abstract string Value { get; }

        public static implicit operator T(FluentlyResemblingA<T> resembling)
        {
            return resembling.ResemblingA;
        }

        public static implicit operator string(FluentlyResemblingA<T> resembling)
        {
            return resembling.Value;
        }
    }
}