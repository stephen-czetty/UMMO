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

namespace UMMO.TestingUtils.RandomData
{
    public class ExtendedRandom : Random, IRandom
    {
        #region IRandom Members

        public byte[] NextBytes( int bufferLength )
        {
            var bytes = new byte[bufferLength];
            NextBytes( bytes );
            return bytes;
        }

        private int NextInt32()
        {
            unchecked
            {
                int firstBits = Next(0, 1 << 4) << 28;
                int lastBits = Next(0, 1 << 28);
                return firstBits | lastBits;
            }
        }

        public decimal NextDecimal()
        {
            bool sign = Next(2) == 1;
            return NextDecimal(sign);
        }

        private decimal NextDecimal(bool sign)
        {
            var scale = (byte)Next(29);
            return new decimal(NextInt32(),
                                NextInt32(),
                                NextInt32(),
                                sign,
                                scale);
        }

        private decimal NextNonNegativeDecimal()
        {
            return NextDecimal(false);
        }

        public decimal NextDecimal(decimal maxValue)
        {
            return (NextNonNegativeDecimal() / Decimal.MaxValue) * maxValue;
        }

        public decimal NextDecimal(decimal minValue, decimal maxValue)
        {
            if (minValue >= maxValue)
                throw new InvalidOperationException();

            // We want to prevent overflows, so if we get a situation that would create one,
            // then change the value to Decimal.MaxValue
            decimal range = ((maxValue == Decimal.MaxValue && minValue < 0) || (minValue == Decimal.MinValue && maxValue > 0)) ? Decimal.MaxValue : maxValue - minValue;
            return NextDecimal(range) + minValue;
        }


        public long NextLong()
        {
            var bytes = new byte[sizeof(long)];
            NextBytes(bytes);
            // strip out the sign bit
            bytes[sizeof(long) - 1] = (byte)(bytes[sizeof(long) - 1] & 0x7f);
            return BitConverter.ToInt64(bytes, 0);
        }

        public long NextLong(long maxValue)
        {
            return (long)((NextLong() / (double)Int64.MaxValue) * maxValue);
        }

        public long NextLong(long minValue, long maxValue)
        {
            if (minValue >= maxValue)
                throw new InvalidOperationException();
            long range = ((maxValue == Int64.MaxValue && minValue < 0) || (minValue == Int64.MinValue && maxValue > 0)) ? Int64.MaxValue : maxValue - minValue;
            return NextLong(range) + minValue;
        }

        #endregion
    }
}