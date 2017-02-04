using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitelist
{
    public class IpAddressRange
    {
        public IpAddress Min;
        public IpAddress Max;

       public IpAddressRange(IpAddress min, IpAddress max)
        {
            if (max < min)
            {
                throw new ArgumentOutOfRangeException();
            }

            Min = min;
            Max = max;
        }

        public bool Contains(IpAddress address)
        {
            return address >= Min && address <= Max;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            IpAddressRange ipAddress = (IpAddressRange)obj;
            return this == ipAddress;
        }

        public static bool TryParse(string input, out IpAddressRange range)
        {
            range = null;
            if (input == null)
            {
                return false;
            }

            var splitInput = input.Split('/');
            if (splitInput.Count() != 2)
            {
                return false;
            }

            var ipAddrSplit = splitInput[0].Split('.');
            if (ipAddrSplit.Count() != 4)
            {
                return false;
            }

            uint bitField;
            if(!uint.TryParse(splitInput[1], out bitField) || bitField < 0 || bitField > 32)
            {
                return false;
            }

            var integerAddress = new uint[4];
            for(int i = 0; i < 4; i++)
            {
                if(!uint.TryParse(ipAddrSplit[i], out integerAddress[i]) || integerAddress[i] < 0 || integerAddress[i] > 255)
                {
                    return false;
                }
            }

            uint finalAddress = 0;
            finalAddress = (finalAddress | integerAddress[0]) << 8;
            finalAddress = (finalAddress | integerAddress[1]) << 8;
            finalAddress = (finalAddress | integerAddress[2]) << 8;
            finalAddress = (finalAddress | integerAddress[3]);

            var flippedBitMask = (uint)Math.Pow(2, 32 - bitField) - 1;
            uint bitMask = uint.MaxValue - flippedBitMask;

            range = new IpAddressRange(new IpAddress(finalAddress & bitMask), new IpAddress(finalAddress | flippedBitMask));
            return true;
        }

        public static bool operator ==(IpAddressRange x, IpAddressRange y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            return x.Min == y.Min && x.Max == y.Max;
        }

        public static bool operator !=(IpAddressRange x, IpAddressRange y)
        {
            return !(x == y);
        }

    }
}
