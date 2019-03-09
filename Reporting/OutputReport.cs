using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Output Report")]
    [Description("Returns the specified set of messages as a DataTable.")]
    public class OutputReport : CodeActivity
    {
        [Category("Filters")]
        [Description("Filters the messages returned to only messages containing all the specified tags.")]
        public InArgument<string[]> TagFilter { get; set; }

        [Category("Filters")]
        [Description("Filters the messages to contain only messages containing any of the specified TransactionIds.")]
        public InArgument<string[]> TransactionIdFilter { get; set; }

        [RequiredArgument]
        [Category("Filters")]
        [Description("The minimum log level to be included in the report.")]
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Info;

        [RequiredArgument]
        [Category("Input")]
        [Description("Determines with the output DataTable will contain typed, complex objects (False), or to convert them to plaintext (True).")]
        public InArgument<bool> Plaintext { get; set; } = true;

        [RequiredArgument]
        [Category("Output")]
        [Description("The resulting DataTable.")]
        public OutArgument<DataTable> DataTable { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var tagFilter = (TagFilter.Get(context) ?? new string[0]).ToList();
            var transactionIdFilter = (TransactionIdFilter.Get(context) ?? new string[0]).ToList();

            if (Plaintext.Get(context))
            {
                DataTable.Set(context, MessageRepository.Instance.CreatePlaintextReport(
                    tagFilter,
                    transactionIdFilter,
                    MinimumLogLevel));
            }
            else
            {
                DataTable.Set(context, MessageRepository.Instance.CreateReport(
                    tagFilter,
                    transactionIdFilter,
                    MinimumLogLevel));
            }
        }
    }
}
