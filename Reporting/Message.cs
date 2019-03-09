using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    public class Message
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Text { get; set; }
        public string TransactionId { get; set; }
        public LogLevel Level { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }
        public bool IgnoreDefaultTags { get; set; }

        public Message(
            LogLevel level, 
            string text, 
            DateTimeOffset timestamp, 
            string transactionId = null, 
            List<string> tags = null,
            Dictionary<string, object> extendedProperties = null,
            bool ignoreDefaultTags = true)
        {
            Level = level;
            Text = text;
            Timestamp = timestamp;
            IgnoreDefaultTags = ignoreDefaultTags;
            CustomProperties = extendedProperties ?? new Dictionary<string, object>();
            TransactionId = transactionId;

            if (IgnoreDefaultTags)
            {
                Tags = tags ?? new List<string>();
            }
            else
            {
                Tags = new List<string>(MessageRepository.Instance.DefaultTags);
                Tags.AddRange(tags ?? new List<string>());
            }
        }

        public bool ShouldShow(List<string> tagFilter, List<string> TransactionIdFilter, LogLevel minimumLogLevel)
        {
            if(minimumLogLevel > Level) {
                return false;
            }

            foreach(var tag in tagFilter)
            {
                if (!Tags.Contains(tag))
                {
                    return false;
                }
            }
            if (TransactionIdFilter.Count > 0 && !TransactionIdFilter.Contains(TransactionId))
            {
                return false;
            }

            return true;
        }
    }
}
