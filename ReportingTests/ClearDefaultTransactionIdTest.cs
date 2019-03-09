using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class ClearDefaultTransactionIdTest
    {
        [TestMethod]
        public void MinimalTest()
        {
            InvokeActivity.SetDefaultTransactionId("123");
            Assert.AreEqual(MessageRepository.Instance.DefaultTransactionId, "123");
            InvokeActivity.ClearDefaultTransactionId();
            Assert.IsNull(MessageRepository.Instance.DefaultTransactionId);

            MessageRepository.Instance.Reset();
        }
    }
}
