using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class SetDefaultTagsTest
    {
        [TestMethod]
        public void MinimalTest()
        {
            InvokeActivity.AddDefaultTags("tag1");

            InvokeActivity.SetDefaultTags("tag2");
            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 1);
            Assert.AreEqual(MessageRepository.Instance.DefaultTags[0], "tag2");

            InvokeActivity.SetDefaultTags(tags: new[] { "tag3", "tag4" });
            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 2);
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains("tag3"));
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains("tag4"));
        }
    }
}

