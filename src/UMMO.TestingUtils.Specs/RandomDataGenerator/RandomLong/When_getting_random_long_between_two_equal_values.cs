﻿#region Copyright

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

using Machine.Specifications;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator.RandomLong
{
    [Subject(typeof(RandomData.RandomLong))]
    public class When_getting_random_long_between_two_equal_values
    {
        private Establish Context = () =>
                                        {
                                            _randomLong = A.Random.LongInteger;
                                            _randomRange = A.Random.LongInteger;
                                        };

        private Because Of = () => _randomValue = _randomLong.Between( _randomRange, _randomRange );

        private It Should_return_value
            = () => _randomValue.ShouldEqual( _randomRange );

        private static RandomData.RandomLong _randomLong;
        private static long _randomValue;
        private static long _randomRange;
    }
}