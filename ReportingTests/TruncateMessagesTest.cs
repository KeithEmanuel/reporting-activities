using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class TruncateMessagesTest
    {
        [TestMethod]
        public void MinimalTest()
        {
            InvokeActivity.AddMessage("HeyHeyHey");
            Assert.AreEqual(MessageRepository.Instance.Messages.Count, 1);
            InvokeActivity.TruncateMessages();
            Assert.AreEqual(MessageRepository.Instance.Messages.Count, 0);
        }
    }
}
