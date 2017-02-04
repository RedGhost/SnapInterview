using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitelist
{
    public class Whitelist
    {
        private List<IpAddressRange> AllowedIpRanges;

        public Whitelist()
        {
            AllowedIpRanges = new List<IpAddressRange>();
        }

        public void AddRange(IpAddressRange range)
        {
            if (range == null)
            {
                throw new ArgumentNullException(nameof(range));
            }

            AllowedIpRanges.Add(range);
        }

        public bool Allowed(IpAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            foreach(var range in AllowedIpRanges)
            {
                if (range.Contains(address))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
