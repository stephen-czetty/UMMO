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
// Copyright 2010-2018, Stephen Michael Czetty
#endregion

using Moq;
using UMMO.TestingUtils.RandomData;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    public abstract class RandomDataGeneratorTestsBase
    {
        protected static readonly IRandom RealRandom = new ExtendedRandom();
        protected readonly Mock<IRandom> Random = new Mock<IRandom>();
        protected readonly TestingUtils.RandomDataGenerator Subject;

        protected RandomDataGeneratorTestsBase()
        {
            Subject = new RandomDataGeneratorAccessor(Random.Object);
        }

        private class RandomDataGeneratorAccessor : TestingUtils.RandomDataGenerator
        {
            protected internal RandomDataGeneratorAccessor(IRandom random) : base(random) { }
        }
    }
}