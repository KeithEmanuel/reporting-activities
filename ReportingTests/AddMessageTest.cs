using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class AddMessageTest
    {
        [TestMethod]
        public void MinimalTest()
        {
            var message = "Hi";

            InvokeActivity.AddMessage(message);

            Assert.AreEqual(MessageRepository.Instance.Messages.Count, 1);
            Assert.AreEqual(MessageRepository.Instance.Messages.First().Text, message);

            MessageRepository.Instance.Messages.Clear();
        }

        //[TestMethod]
        public void AddLotsOfMessagesTest()
        {
            var numberOfMessages = 1_000_000;

            foreach(var i in Enumerable.Range(0, numberOfMessages))
            {
                InvokeActivity.AddMessage("This is a kinda looooooooong messsage. Kinda. Not really.");
            }

            Assert.AreEqual(MessageRepository.Instance.Messages.Count, numberOfMessages);
            MessageRepository.Instance.Messages.Clear();
        }
    }
}
