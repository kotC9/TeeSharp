using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeeSharp.Network;
using TeeSharp.Network.Extensions;

namespace TeeSharp.Tests
{
    [TestClass]
    public class NetworkTests
    {
        [TestMethod]
        public void TestIPv4EndPointSerialize()
        {
            var endPoint1 = new IPEndPoint(IPAddress.Parse("192.168.42.5"), 65202);
            var buf1 = new byte[] {1, 0, 0, 0, 192, 168, 42, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 178, 254, 0, 0};
            var buf2 = endPoint1.Raw();
            CollectionAssert.AreEqual(buf1, buf2);
            
            // TODO for IPv6
            //var endPoint2 = new IPEndPoint(IPAddress.Parse("2001:0db8:85a3:0000:0000:8a2e:0370:7334"), 65202);
            // var buf3 = endPoint2.Raw();
        }

        [TestMethod]
        public void TestHash()
        {
            var endPoint1 = new IPEndPoint(IPAddress.Parse("192.168.42.5"), 65202);
            var buf2 = endPoint1.Raw();
            var hash = Network.NetworkHelper.Hash(buf2);

            Assert.AreEqual(hash, 1578705565u);
        }

        [TestMethod]
        public void TestGenerateToken()
        {
            var endPoint1 = new IPEndPoint(IPAddress.Parse("192.168.42.5"), 65202);
            var token = TokenHelper.GenerateToken(endPoint1, 666777888);

            Assert.AreEqual(token, 2215451845u);
        }
    }
}
