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

using log4net;
using log4net.Core;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Annotations;

namespace UMMO.Extensions.Specs.Log4NetExtensions
{
    public class Log4NetExtensionsSpecsBase : WithFakes
    {
        [UsedImplicitly]
        private Establish Context = () =>
                                        {
                                            The<ILog>().WhenToldTo( x => x.Logger ).Return( The<ILogger> );
                                        };

        protected static object ReturnValue;
    }
}