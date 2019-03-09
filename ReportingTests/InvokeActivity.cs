using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporting;

namespace ReportingTests
{
    internal static class InvokeActivity
    {
        public static IDictionary<string, object> AddDefaultTags(string tag = null, string[] tags = null)
        {
            var addDefaultTags = new AddDefaultTags();
            var args = new Dictionary<string, object>() { { "Tag", tag }, { "Tags", tags } };
            return WorkflowInvoker.Invoke(addDefaultTags, args);
        }

        public static IDictionary<string, object> AddMessage(
            string message, 
            string transactionId = null, 
            LogLevel level = LogLevel.Verbose, 
            string[] tags = null, 
            Dictionary<string, object> customProperties = null, 
            bool ignoreDefaultTags = false)
        {
            var addMessage = new AddMessage();
            addMessage.Level = level;
            var args = new Dictionary<string, object>() {
                { "Message", message },
                { "TransactionId", transactionId },
                { "Tags", tags },
                { "CustomProperties", customProperties },
                { "IgnoreDefaultTags", ignoreDefaultTags }
            };

            return WorkflowInvoker.Invoke(addMessage, args);
        }

        public static IDictionary<string, object> ClearDefaultTags()
        {
            return WorkflowInvoker.Invoke(new ClearDefaultTags());
        }

        public static IDictionary<string, object> ClearDefaultTransactionId()
        {
            return WorkflowInvoker.Invoke(new ClearDefaultTransactionId());
        }

        public static IDictionary<string, object> OutputReport(string[] tagFilter = null, string[] transactionIdFilter = null, LogLevel minLogLevel = LogLevel.Info, bool plaintext = true)
        {
            var outputReport = new OutputReport();
            outputReport.MinimumLogLevel = minLogLevel;
            var args = new Dictionary<string, object>()
            {
                { "TagFilter", tagFilter },
                { "TransactionIdFilter", transactionIdFilter },
                { "Plaintext", plaintext },
            };

            return WorkflowInvoker.Invoke(outputReport, args);
        }

        public static IDictionary<string, object> RemoveDefaultTags(string tag = null, string[] tags = null)
        {
            var removeDefaultTags = new RemoveDefaultTags();
            var args = new Dictionary<string, object>() { { "Tag", tag }, { "Tags", tags } };
            return WorkflowInvoker.Invoke(removeDefaultTags, args);
        }

        public static IDictionary<string, object> SetDefaultTags(string tag = null, string[] tags = null)
        {
            var setDefaultTags = new SetDefaultTags();
            var args = new Dictionary<string, object>() { { "Tag", tag }, { "Tags", tags } };
            return WorkflowInvoker.Invoke(setDefaultTags, args);
        }

        public static IDictionary<string, object> SetDefaultTransactionId(string transactionId)
        {
            var setDefaultTags = new SetDefaultTransactionId();
            var args = new Dictionary<string, object>() { { "TransactionId", transactionId } };
            return WorkflowInvoker.Invoke(setDefaultTags, args);
        }

        public static IDictionary<string, object> TruncateMessages()
        {
            var setDefaultTags = new TruncateMessages();
            return WorkflowInvoker.Invoke(setDefaultTags);
        }
    }
}
