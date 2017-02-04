using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whitelist;

namespace WhitelistTests
{
    [TestClass]
    public class IpAddressRangeTests
    {
        [TestMethod]
        public void TryParse_Null()
        {
            // Setup
            string input = null;

            // Action
            IpAddressRange result;
            var success = IpAddressRange.TryParse(input, out result);

            // Verify
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TryParse_IncorrectFormat()
        {
            // Setup
            string input = "1231";

            // Action
            IpAddressRange result;
            var success = IpAddressRange.TryParse(input, out result);

            // Verify
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void TryParse_MinToMaxValue()
        {
            // Setup
            var expected = new IpAddressRange(new IpAddress(0), new IpAddress(uint.MaxValue));
            string input = "0.0.0.0/0";

            // Action
            IpAddressRange result;
            var success = IpAddressRange.TryParse(input, out result);

            // Verify
            Assert.IsTrue(success);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryParse_MaxValue()
        {
            // Setup
            var expected = new IpAddressRange(new IpAddress(uint.MaxValue), new IpAddress(uint.MaxValue));
            string input = "255.255.255.255/32";

            // Action
            IpAddressRange result;
            var success = IpAddressRange.TryParse(input, out result);

            // Verify
            Assert.IsTrue(success);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryParse_MinValue()
        {
            // Setup
            var expected = new IpAddressRange(new IpAddress(0), new IpAddress(0));
            string input = "0.0.0.0/32";

            // Action
            IpAddressRange result;
            var success = IpAddressRange.TryParse(input, out result);

            // Verify
            Assert.IsTrue(success);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryParse_ValidValue()
        {
            // Setup
            var expected = new IpAddressRange(new IpAddress(3232238080), new IpAddress(3232238335));
            string input = "192.168.10.1/24";

            // Action
            IpAddressRange result;
            var success = IpAddressRange.TryParse(input, out result);

            // Verify
            Assert.IsTrue(success);
            Assert.AreEqual(expected, result);
        }
    }
}
