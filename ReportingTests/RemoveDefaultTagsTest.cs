using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class RemoveDefaultTagsTest
    {
        [TestMethod]
        public void MinimalTest()
        {
            InvokeActivity.AddDefaultTags(tags: new[] { "hi", "bye" });
            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 2);
            InvokeActivity.RemoveDefaultTags(tags: new[] { "hi", "hey" });
            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 1);
            InvokeActivity.RemoveDefaultTags(tags: new[] { "bye" });
            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 0);

            MessageRepository.Instance.Reset();
        }
    }
}
