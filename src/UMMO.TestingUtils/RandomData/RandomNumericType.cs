#region Copyright

// This file is part of UMMO.
// 
// UMMO is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// UMMO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with UMMO.  If not, see <http://www.gnu.org/licenses/>.
//  
// Copyright 2010, Stephen Michael Czetty

#endregion

using System;
using System.Reflection;

namespace UMMO.TestingUtils.RandomData
{
    /// <summary>
    /// Base class for fluent random classes
    /// </summary>
    /// <remarks>
    /// There is no interface for this because we cannot write an implicit cast on interfaces
    /// </remarks>
    /// <typeparam name="T">The type of random value being created</typeparam>
    public abstract class RandomNumericType<T> where T : struct, IComparable<T>
    {
        /// <summary>
        /// The random number generator
        /// </summary>
        protected readonly IRandom Random;
        private readonly T _maxValue;
        private readonly T _minValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomNumericType&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        /// <exception cref="RandomDataException">
        ///     Thrown when <typeparamref name="T"/> does not define "MinValue" or "MaxValue" fields.
        /// </exception>
        protected RandomNumericType(IRandom random)
        {
            Random = random;
            var minValueField = typeof(T).GetTypeInfo().GetField("MinValue", BindingFlags.Static | BindingFlags.Public);
            var maxValueField = typeof(T).GetTypeInfo().GetField("MaxValue", BindingFlags.Static | BindingFlags.Public);

            if ( minValueField == null || maxValueField == null )
                // The type is not compatible with this class.
                throw new RandomDataException($"Could not create random data for type {typeof(T).FullName}");
            _minValue = (T)minValueField.GetValue( new T() );
            _maxValue = (T)maxValueField.GetValue( new T() );
        }

        // For test coverage
        [Obsolete("The parameter-less constructor should only be used for test coverage")]
        internal RandomNumericType() {}

        public abstract T Value { get; }

        public T Between(T minValue, T maxValue)
        {
            if ( minValue.CompareTo(maxValue) > 0 )
                throw new ArgumentException("minValue must be less than or equal to maxValue");

            return GetBetween(minValue, maxValue);
        }

        public T GreaterThan(T minValue)
        {
            return Between(minValue, _maxValue);
        }

        public T LessThan(T maxValue)
        {
            return Between( _minValue, maxValue );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UMMO.TestingUtils.RandomData.RandomNumericType&lt;T&gt;"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="randomNumeric">The random numeric.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator T(RandomNumericType<T> randomNumeric)
        {
            return randomNumeric.Value;
        }

        /// <summary>
        /// Return a random value of type <typeparamref name="T"/> between the minimum and maximum.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        protected abstract T GetBetween(T minValue, T maxValue);
    }
}