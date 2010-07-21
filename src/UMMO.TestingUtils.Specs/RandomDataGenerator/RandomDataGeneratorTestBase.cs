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
using Machine.Specifications;
using Machine.Specifications.Annotations;
using Rhino.Mocks;
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    public abstract class RandomDataGeneratorTestBase
    {
        protected static RandomData.RandomDataGenerator RandomDataGeneratorUnderTest;
        protected static IRandom Random;

        [UsedImplicitly]
        private Establish Context = () =>
                                        {
                                            Random = MockRepository.GenerateStub< IRandom >();
                                            RandomDataGeneratorUnderTest = new RandomDataGeneratorAccessor( Random );
                                        };

        #region Nested type: RandomDataGeneratorAccessor

        // Used to get at the protected internal constructor of RandomDataGenerator.
        private class RandomDataGeneratorAccessor : RandomData.RandomDataGenerator
        {
            protected internal RandomDataGeneratorAccessor( IRandom random ) : base( random ) {}
        }

        #endregion
    }

    [Subject(typeof(RandomDecimal))]
    public class When_getting_random_decimal : RandomDataGeneratorTestBase
    {
        private const decimal ExpectedDecimal = 22.5m;
        private Establish Context = () => Random.Stub( x => x.NextDecimal() ).Return( ExpectedDecimal );
        private Because Of = () => _randomDecimal = RandomDataGeneratorUnderTest.Decimal;

        private It Should_be_of_type_random_decimal
            = () => _randomDecimal.ShouldBeOfType< RandomDecimal >();

        private It Should_return_expected_decimal_when_calling_value
            = () => ( (RandomDecimal)_randomDecimal ).Value.ShouldEqual( ExpectedDecimal );

        private It Should_implicitly_cast_to_decimal
            = () =>
                  {
                      decimal value = (RandomDecimal)_randomDecimal;
                      value.ShouldEqual( ExpectedDecimal );
                  };

        private static Object _randomDecimal;
    }
}