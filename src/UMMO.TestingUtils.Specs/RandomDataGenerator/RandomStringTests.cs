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

using FluentAssertions;
using Xunit;

namespace UMMO.TestingUtils.Specs.RandomDataGenerator
{
    public class RandomStringTests : RandomDataGeneratorTestsBase
    {
        [Fact]
        public void When_getting_random_string()
        {
            string randomString = Subject.String;

            randomString.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void When_getting_a_first_name()
        {
            A.Random.String.ResemblingA.FirstName.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void When_getting_a_last_name()
        {
            A.Random.String.ResemblingA.LastName.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void When_getting_a_password()
        {
            A.Random.String.ResemblingA.Password.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void When_getting_a_noun()
        {
            A.Random.String.ResemblingA.Noun.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void When_getting_a_verb()
        {
            A.Random.String.ResemblingA.Verb.Should().NotBeNullOrWhiteSpace();
        }
    }
}