using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitelist
{
    class Program
    {
        static void Main(string[] args)
        {
            var allowedRanges = new string[]
            {
                "192.168.1.0/24"
            };

            var input = "192.168.1.7";

            IpAddress inputAddr;
            if (!IpAddress.TryParse(input, out inputAddr))
            {
                throw new ArgumentException(nameof(input));
            }

            var whitelist = new Whitelist();
            foreach(var range in allowedRanges)
            {
                IpAddressRange newRange;
                if(!IpAddressRange.TryParse(range, out newRange))
                {
                    throw new ArgumentException(nameof(allowedRanges));
                }

                whitelist.AddRange(newRange);
            }

            if (whitelist.Allowed(inputAddr))
            {
                Console.WriteLine("Address allowed!");
            }
            else
            {
                Console.WriteLine("Address not allowed!");
            }

            Console.ReadLine();
        }
    }
}
