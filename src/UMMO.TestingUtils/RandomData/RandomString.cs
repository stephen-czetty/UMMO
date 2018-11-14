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
using System.Text;
using UMMO.TestingUtils.RandomData.Waffle;

namespace UMMO.TestingUtils.RandomData
{
    /// <summary>
    /// Fluent random string.
    /// </summary>
    /// <remarks>
    /// There is no IRandomString interface because we don't have implicit casting of interfaces.
    /// </remarks>
    public class RandomString : IResemblingA<RandomString>
    {
        private readonly WaffleEngine _waffleEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomString"/> class.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomString( IRandom random )
        {
            _waffleEngine = new WaffleEngine(random);
        }

        [Obsolete("Use ResemblingA instead")]
        public IResemblingA<RandomString> Resembling => this;

        [Obsolete("Use ResemblingA instead")]
        public RandomString A => this;

        public RandomString ResemblingA => this;

        public string FirstName => GetWaffle( "|f" );

        public string LastName => GetWaffle( "|s" );

        public string Password => GetWaffle( "|ue|ud" );

        public string Noun => GetWaffle( "|o" );

        public string Verb => GetWaffle( "|d" );

        /// <summary>
        /// Performs an implicit conversion from <see cref="RandomString"/> to <see cref="string"/>.
        /// </summary>
        /// <param name="randomString">The random string.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string( RandomString randomString )
        {
            return randomString.GetWaffle("|e|d|o");
        }

        private string GetWaffle( string phrase )
        {
            var stringBuilder = new StringBuilder();
            _waffleEngine.EvaluatePhrase( phrase, stringBuilder );
            return stringBuilder.ToString();
        }
    }
}