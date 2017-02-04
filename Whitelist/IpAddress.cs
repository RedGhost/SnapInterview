using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitelist
{
    public class IpAddress
    {
        private uint IpAddrNumber;

        public IpAddress(uint ipAddrNum)
        {
            IpAddrNumber = ipAddrNum;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            IpAddress ipAddress = (IpAddress)obj;
            return this == ipAddress;
        }

        public override int GetHashCode()
        {
            return IpAddrNumber.GetHashCode();
        }

        public static bool TryParse(string ipAddrStr, out IpAddress ipAddress)
        {
            ipAddress = null;
            if (ipAddrStr == null)
            {
                return false;
            }

            var ipAddrSplit = ipAddrStr.Split('.');
            if (ipAddrSplit.Count() != 4)
            {
                return false;
            }

            var integerAddress = new uint[4];
            for (int i = 0; i < 4; i++)
            {
                if (!uint.TryParse(ipAddrSplit[i], out integerAddress[i]) || integerAddress[i] < 0 || integerAddress[i] > 255)
                {
                    return false;
                }
            }

            uint finalAddress = 0;
            finalAddress = (finalAddress | integerAddress[0]) << 8;
            finalAddress = (finalAddress | integerAddress[1]) << 8;
            finalAddress = (finalAddress | integerAddress[2]) << 8;
            finalAddress = (finalAddress | integerAddress[3]);

            ipAddress = new IpAddress(finalAddress);

            return true;
        }

        public static bool operator ==(IpAddress x, IpAddress y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            return x.IpAddrNumber == y.IpAddrNumber;
        }

        public static bool operator !=(IpAddress x, IpAddress y)
        {
            return !(x == y);
        }

        public static bool operator <(IpAddress x, IpAddress y)
        {
            if (x == null)
            {
                return false;
            }
            if (y == null)
            {
                return true;
            }

            return x.IpAddrNumber < y.IpAddrNumber;
        }

        public static bool operator >(IpAddress x, IpAddress y)
        {
            if (y == null)
            {
                return false;
            }
            if (x == null)
            {
                return true;
            }

            return x.IpAddrNumber > y.IpAddrNumber;
        }

        public static bool operator <=(IpAddress x, IpAddress y)
        {
            return x < y || x == y;
        }

        public static bool operator >=(IpAddress x, IpAddress y)
        {
            return x > y || x == y;
        }

    }
}
