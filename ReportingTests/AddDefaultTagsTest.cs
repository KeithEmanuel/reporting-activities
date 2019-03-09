using System;
using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class AddDefaultTagsTest
    {

        [TestMethod]
        public void AddSingleTagTest()
        {
            var tag = "some-tag";
            InvokeActivity.AddDefaultTags(tag: tag);

            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 1);
            Assert.AreEqual(MessageRepository.Instance.DefaultTags[0], tag);

            MessageRepository.Instance.Reset();
        }

        [TestMethod]
        public void AddMultipleTagsTest()
        {
            var tags = new[] { "tag1", "tag2" };
            InvokeActivity.AddDefaultTags(tags: tags);

            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 2);
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains(tags[0]));
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains(tags[1]));

            MessageRepository.Instance.Reset();
        }

        [TestMethod]
        public void IgnoreTagIfTagsProvided()
        {
            var tag = "tag0";
            var tags = new[] { "tag1", "tag2" };
            InvokeActivity.AddDefaultTags(tag, tags);

            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 2);
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains(tags[0]));
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains(tags[1]));
            Assert.IsFalse(MessageRepository.Instance.DefaultTags.Contains(tag));

            MessageRepository.Instance.Reset();
        }

        [TestMethod]
        public void TagsMadeDistinctTest()
        {
            var tags = new[] { "tag1", "tag2", "tag1", "tag2", "tag3" };
            InvokeActivity.AddDefaultTags(tags: tags);

            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 3);
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains("tag1"));
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains("tag2"));
            Assert.IsTrue(MessageRepository.Instance.DefaultTags.Contains("tag3"));

            MessageRepository.Instance.Reset();
        }
    }
}
