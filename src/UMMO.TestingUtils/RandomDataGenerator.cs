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
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils
{
    /// <summary>
    /// Class for syntactic sugar returned by A.Random
    /// </summary>
    public class RandomDataGenerator
    {
        private readonly IRandom _random;
        private readonly Lazy<RandomNumericType<int>> _randomInteger;
        private readonly Lazy<FluentlyResemblingA<RandomString>> _randomString;
        private readonly Lazy<RandomNumericType<decimal>> _randomDecimal;
        private readonly Lazy<RandomNumericType<double>> _randomDouble;
        private readonly Lazy<RandomNumericType<long>> _randomLong;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomDataGenerator"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        protected internal RandomDataGenerator(IRandom random)
        {
            _random = random;
            _randomString = new Lazy<FluentlyResemblingA<RandomString>>(() => new RandomString(_random));
            _randomInteger = new Lazy<RandomNumericType<int>>(() => new RandomInteger(_random));
            _randomLong = new Lazy<RandomNumericType<long>>(() => new RandomLong(_random));
            _randomDecimal = new Lazy<RandomNumericType<decimal>>(() => new RandomDecimal(_random));
            _randomDouble = new Lazy<RandomNumericType<double>>(() => new RandomDouble(_random));
        }

        /// <summary>
        /// A random fluent string.
        /// </summary>
        /// <value>The string.</value>
        public FluentlyResemblingA<RandomString> String => _randomString.Value;

        /// <summary>
        /// A random fluent integer.
        /// </summary>
        /// <value>The integer.</value>
        public RandomNumericType<int> Integer => _randomInteger.Value;

        /// <summary>
        /// A random boolean.
        /// </summary>
        /// <value>The boolean.</value>
        public bool Boolean => _random.NextDouble() >= 0.5;

        /// <summary>
        /// A random byte.
        /// </summary>
        /// <value>The byte.</value>
        public byte Byte => _random.NextBytes(1)[0];

        /// <summary>
        /// A random character.
        /// </summary>
        /// <value>The character.</value>
        public char Character => String.ResemblingA.Password[0];

        /// <summary>
        /// A random GUID.
        /// </summary>
        /// <value>The GUID.</value>
        public Guid Guid => Guid.NewGuid();

        /// <summary>
        /// A random short.
        /// </summary>
        /// <value>The short.</value>
        public short Short => (short)_random.Next();

        /// <summary>
        /// A random fluent long integer.
        /// </summary>
        /// <value>The long integer.</value>
        public RandomNumericType<long> LongInteger => _randomLong.Value;

        /// <summary>
        /// A random float.
        /// </summary>
        /// <value>The float.</value>
        public float Float => (float)(_random.NextDouble() * _random.Next());

        /// <summary>
        /// A random fluent double.
        /// </summary>
        /// <value>The double.</value>
        public RandomNumericType<double> Double => _randomDouble.Value;

        /// <summary>
        /// A random fluent decimal.
        /// </summary>
        /// <value>The decimal.</value>
        public RandomNumericType<decimal> Decimal => _randomDecimal.Value;

        /// <summary>
        /// A random date time.
        /// </summary>
        /// <value>The date time.</value>
        public DateTime DateTime => new DateTime( _random.Next( 1970, 2100 ), _random.Next( 1, 12 ), _random.Next( 1, 28 ) );
    }
    // ReSharper restore MemberCanBeMadeStatic.Global

    /// <summary>
    /// Utility class to simplify test code
    /// </summary>
    public static partial class A
    {
        private static readonly Lazy<RandomDataGenerator> RandomDataGenerator =
            new Lazy<RandomDataGenerator>(() => new RandomDataGenerator(new ExtendedRandom()));

        /// <summary>
        /// Fluent accessor for random data.
        /// </summary>
        /// <value>The random data accessor.</value>
        public static RandomDataGenerator Random => RandomDataGenerator.Value;
    }
}