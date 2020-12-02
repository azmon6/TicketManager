using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //TODO Unit Test Role Provider
        [TestMethod]
        public void Working()
        {
            Assert.AreEqual(5, 5);
        }

        [TestMethod]
        public void NotWorking()
        {
            Assert.AreEqual(5, 6);
        }
    }
}
