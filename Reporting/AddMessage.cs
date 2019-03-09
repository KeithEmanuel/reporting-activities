using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Add Message")]
    [Description("Adds a message to the an internal message repository. This does not call the default log message activity. Retrieve the messages by using 'OutputReport'.")]
    public class AddMessage : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("The message text.")]
        public InArgument<string> Message { get; set; }

        [Category("Input")]
        [Description("The associated TransactionId. This will override any default set.")]
        public InArgument<string> TransactionId { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The Logging level.")]
        public LogLevel Level { get; set; } = LogLevel.Verbose;

        [Category("Input")]
        [Description("Any tags you wish to add to the message. These can be filtered on using 'Output Logs'.")]
        public InArgument<string[]> Tags { get; set; }

        [Category("Input")]
        [Description("Dictionary of any key (string) value (object) pairs that you wish to include with this message.")]
        public InArgument<Dictionary<string, object>> CustomProperties { get; set; }

        [Category("Input")]
        [Description("Boolean indicating whether default tags should be ignored (not added) for this message.")]
        public InArgument<bool> IgnoreDefaultTags { get; set; } = false;

        protected override void Execute(CodeActivityContext context)
        {
            MessageRepository.Instance.Messages.Add(
                new Message(
                    Level,
                    Message.Get(context),
                    DateTimeOffset.Now,
                    TransactionId.Get(context) ?? MessageRepository.Instance.DefaultTransactionId,
                    (Tags.Get(context) ?? new string[0]).ToList(),
                    CustomProperties.Get(context),
                    IgnoreDefaultTags.Get(context)));
        }
    }
}
