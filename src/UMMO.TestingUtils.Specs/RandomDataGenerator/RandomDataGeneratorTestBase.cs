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

using JetBrains.Annotations;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Annotations;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    public abstract class RandomDataGeneratorTestBase : WithFakes
    {
        [UsedImplicitly]
        private Establish Context = () =>
                                        {
                                            Random = The<IRandom>();
                                            RandomDataGeneratorUnderTest = new RandomDataGeneratorAccessor( Random );
                                        };

        protected static TestingUtils.RandomDataGenerator RandomDataGeneratorUnderTest;
        protected static IRandom Random;

        #region Nested type: RandomDataGeneratorAccessor

        // Used to get at the protected internal constructor of RandomDataGenerator.
        private class RandomDataGeneratorAccessor : TestingUtils.RandomDataGenerator
        {
            protected internal RandomDataGeneratorAccessor( IRandom random ) : base( random ) {}
        }

        #endregion
    }
}