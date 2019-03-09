using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class SetDefaultTransactionIdTest
    {
        [TestMethod]
        public void MinimalTest()
        {
            InvokeActivity.SetDefaultTransactionId("1");
            Assert.AreEqual(MessageRepository.Instance.DefaultTransactionId, "1");

            MessageRepository.Instance.Reset();

        }
    }
}
