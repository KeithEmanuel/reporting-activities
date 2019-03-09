using System;
using System.Activities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;

namespace ReportingTests
{
    [TestClass]
    public class ClearDefaultTagsTest
    {
        [TestMethod]
        public void MinimalTest()
        {
            InvokeActivity.AddDefaultTags("some-tag");
            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 1);
            InvokeActivity.ClearDefaultTags();
            Assert.AreEqual(MessageRepository.Instance.DefaultTags.Count, 0);
        }
    }
}
