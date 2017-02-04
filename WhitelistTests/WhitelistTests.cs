using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whitelist;

namespace WhitelistTests
{
    [TestClass]
    public class WhitelistTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRange_Null()
        {
            // Setup
            var whitelist = new Whitelist.Whitelist();

            // Action
            whitelist.AddRange(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Allowed_Null()
        {
            // Setup
            var whitelist = new Whitelist.Whitelist();

            // Action
            whitelist.Allowed(null);
        }

        [TestMethod]
        public void Allowed_InRange()
        {
            // Setup
            var whitelist = new Whitelist.Whitelist();
            var ipRange = new IpAddressRange(new IpAddress(0), new IpAddress(100));
            var ipAddr = new IpAddress(50);

            // Action
            whitelist.AddRange(ipRange);
            var allowed = whitelist.Allowed(ipAddr);

            // Verify
            Assert.IsTrue(allowed);
        }

        [TestMethod]
        public void Allowed_NoRanges()
        {
            // Setup
            var whitelist = new Whitelist.Whitelist();
            var ipAddr = new IpAddress(2000);

            // Action
            var allowed = whitelist.Allowed(ipAddr);

            // Verify
            Assert.IsFalse(allowed);
        }

        [TestMethod]
        public void Allowed_OutRange()
        {
            // Setup
            var whitelist = new Whitelist.Whitelist();
            var ipRange = new IpAddressRange(new IpAddress(0), new IpAddress(100));
            var ipAddr = new IpAddress(2000);

            // Action
            whitelist.AddRange(ipRange);
            var allowed = whitelist.Allowed(ipAddr);

            // Verify
            Assert.IsFalse(allowed);
        }

        [TestMethod]
        public void Allowed_InFirstRangeOfTwo()
        {
            // Setup
            var whitelist = new Whitelist.Whitelist();
            var ipRange1 = new IpAddressRange(new IpAddress(0), new IpAddress(100));
            var ipRange2 = new IpAddressRange(new IpAddress(100), new IpAddress(5000));

            var ipAddr = new IpAddress(50);

            // Action
            whitelist.AddRange(ipRange1);
            whitelist.AddRange(ipRange2);
            var allowed = whitelist.Allowed(ipAddr);

            // Verify
            Assert.IsTrue(allowed);
        }

        [TestMethod]
        public void Allowed_InSecondRangeOfTwo()
        {
            // Setup
            var whitelist = new Whitelist.Whitelist();
            var ipRange1 = new IpAddressRange(new IpAddress(100), new IpAddress(5000));
            var ipRange2 = new IpAddressRange(new IpAddress(0), new IpAddress(100));

            var ipAddr = new IpAddress(50);

            // Action
            whitelist.AddRange(ipRange1);
            whitelist.AddRange(ipRange2);
            var allowed = whitelist.Allowed(ipAddr);

            // Verify
            Assert.IsTrue(allowed);
        }
    }
}
