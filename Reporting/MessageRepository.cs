using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    public class MessageRepository
    {
        public static MessageRepository Instance { get; } = new MessageRepository();

        public List<Message> Messages { get; set; } = new List<Message>();

        public List<string> DefaultTags { get; set; } = new List<string>();

        public string DefaultTransactionId { get; set; } = null;

        public void Reset()
        {
            Messages = new List<Message>();
            DefaultTags = new List<string>();
            DefaultTransactionId = null;
        }

        public DataTable CreateReport(List<string> tagFilter, List<string> transactionIdFilter, LogLevel minimumLogLevel)
        {
            var table = CreateDataTable();

            if(tagFilter == null)
            {
                tagFilter = new List<string>();
            }
            if(transactionIdFilter == null)
            {
                transactionIdFilter = new List<String>();
            }

            foreach(Message message in Messages)
            {
                if(message.ShouldShow(tagFilter, transactionIdFilter, minimumLogLevel))
                {
                    var newRow = table.NewRow();
                    newRow["Message"] = message.Text;
                    newRow["Level"] = message.Level;
                    newRow["Timestamp"] = message.Timestamp;
                    newRow["TransactionId"] = message.TransactionId;
                    newRow["Tags"] = message.Tags;
                    newRow["ExtendedProperties"] = message.CustomProperties;
                    
                    table.Rows.Add(newRow);
                }
            }

            return table;
        }

        public DataTable CreatePlaintextReport(List<string> tagFilter, List<string> transactionIdFilter, LogLevel minimumLogLevel)
        {
            var table = CreatePlaintextDataTable();

            if (tagFilter == null)
            {
                tagFilter = new List<string>();
            }
            if (transactionIdFilter == null)
            {
                transactionIdFilter = new List<string>();
            }

            foreach (Message message in Messages)
            {
                foreach (var key in message.CustomProperties.Keys)
                {
                    if (!table.Columns.Contains(key))
                    {
                        table.Columns.Add(key, typeof(string));
                    }
                }

                if (message.ShouldShow(tagFilter, transactionIdFilter, minimumLogLevel))
                {
                    var newRow = table.NewRow();
                    newRow["Message"] = message.Text;
                    newRow["Level"] = message.Level.ToString();
                    newRow["Timestamp"] = message.Timestamp.ToString();
                    newRow["TransactionId"] = message.TransactionId;
                    newRow["Tags"] = string.Join(", ", message.Tags);

                    foreach(var kvp in message.CustomProperties)
                    {
                        newRow[kvp.Key] = kvp.Value.ToString();
                    }

                    table.Rows.Add(newRow);
                }
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable("Logs");
            table.Columns.Add("Timestamp", typeof(DateTimeOffset));
            table.Columns.Add("Level", typeof(LogLevel));
            table.Columns.Add("Message", typeof(string));
            table.Columns.Add("TransactionId", typeof(string));
            table.Columns.Add("Tags", typeof(List<string>));
            table.Columns.Add("ExtendedProperties", typeof(Dictionary<string, string>));
            return table;
        }

        private static DataTable CreatePlaintextDataTable()
        {
            DataTable table = new DataTable("Logs");
            table.Columns.Add("Timestamp", typeof(string));
            table.Columns.Add("Level", typeof(string));
            table.Columns.Add("Message", typeof(string));
            table.Columns.Add("TransactionId", typeof(string));
            table.Columns.Add("Tags", typeof(string));
            return table;
        }
    }
}
