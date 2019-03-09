using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class OutputReportTest
    {
        [TestMethod]
        public void MinimalTest()
        {
            InvokeActivity.AddMessage(":)", level: LogLevel.Info);
            var table = (DataTable)InvokeActivity.OutputReport(minLogLevel: LogLevel.Info)["DataTable"];

            Assert.AreEqual(table.Rows.Count, 1);

            MessageRepository.Instance.Reset();
        }

        [TestMethod]
        public void TagFilterTest()
        {
            InvokeActivity.AddMessage(":)", tags: new[] { "face", "positive" }, level: LogLevel.Info);
            InvokeActivity.AddMessage(":(", tags: new[] { "face", "negative" }, level: LogLevel.Info);
            InvokeActivity.AddMessage("Good", tags: new[] { "word", "positive" }, level: LogLevel.Info);

            var allMessages = (DataTable)InvokeActivity.OutputReport()["DataTable"];
            Assert.AreEqual(allMessages.Rows.Count, 3);

            var faceMessages = (DataTable)InvokeActivity.OutputReport(tagFilter: new[] { "face" })["DataTable"];
            Assert.AreEqual(faceMessages.Rows.Count, 2);

            var wordMessages = (DataTable)InvokeActivity.OutputReport(tagFilter: new[] { "word" })["DataTable"];
            Assert.AreEqual(wordMessages.Rows.Count, 1);

            var positiveMessages = (DataTable)InvokeActivity.OutputReport(tagFilter: new[] { "positive" })["DataTable"];
            Assert.AreEqual(positiveMessages.Rows.Count, 2);

            var negativeMessages = (DataTable)InvokeActivity.OutputReport(tagFilter: new[] { "negative" })["DataTable"];
            Assert.AreEqual(negativeMessages.Rows.Count, 1);

            var positiveWordsMessages = (DataTable)InvokeActivity.OutputReport(tagFilter: new[] { "positive", "word" })["DataTable"];
            Assert.AreEqual(positiveWordsMessages.Rows.Count, 1);

            var noMessages = (DataTable)InvokeActivity.OutputReport(tagFilter: new[] { "positive", "negative" })["DataTable"];
            Assert.AreEqual(noMessages.Rows.Count, 0);

            MessageRepository.Instance.Reset();
        }

        [TestMethod]
        public void TransactionIdFilterTest()
        {
            InvokeActivity.AddMessage(":) 1", transactionId: "1", level: LogLevel.Info);
            InvokeActivity.AddMessage(":) 2", transactionId: "2", level: LogLevel.Info);
            InvokeActivity.AddMessage(":) 2", transactionId: "2", level: LogLevel.Info);
            InvokeActivity.AddMessage(":) 3", transactionId: "3", level: LogLevel.Info);

            var allMessages = (DataTable)InvokeActivity.OutputReport()["DataTable"];
            Assert.AreEqual(allMessages.Rows.Count, 4);

            var id1 = (DataTable)InvokeActivity.OutputReport(transactionIdFilter: new []{ "1" })["DataTable"];
            Assert.AreEqual(id1.Rows.Count, 1);

            var id2 = (DataTable)InvokeActivity.OutputReport(transactionIdFilter: new[] { "2" })["DataTable"];
            Assert.AreEqual(id2.Rows.Count, 2);

            var id1and2 = (DataTable)InvokeActivity.OutputReport(transactionIdFilter: new[] { "1", "2" })["DataTable"];
            Assert.AreEqual(id1and2.Rows.Count, 3);

            MessageRepository.Instance.Reset();
        }

        [TestMethod]
        public void CustomPropertiesTest()
        {
            var props = new System.Collections.Generic.Dictionary<string, object>() { { "Face", "Happy" } };
            InvokeActivity.AddMessage(":)", customProperties: props, level: LogLevel.Info);

            props = new System.Collections.Generic.Dictionary<string, object>() { { "Face", "Sad" } };
            InvokeActivity.AddMessage(":(", customProperties: props, level: LogLevel.Info);

            props = new System.Collections.Generic.Dictionary<string, object>() { { "Palindrome", true } };
            InvokeActivity.AddMessage("TacoCat", customProperties: props, level: LogLevel.Info);

            var allMessages = (DataTable)InvokeActivity.OutputReport()["DataTable"];
            Assert.AreEqual(allMessages.Rows.Count, 3);
            Assert.IsTrue(allMessages.Columns.Contains("Face"));
            Assert.IsTrue(allMessages.Columns.Contains("Palindrome"));
            Assert.AreEqual(allMessages.Select("Message = 'TacoCat'").First()["Face"], DBNull.Value);
            Assert.AreEqual(allMessages.Select("Message = 'TacoCat'").First()["Palindrome"], true.ToString());

            MessageRepository.Instance.Reset();

        }
    }
}
