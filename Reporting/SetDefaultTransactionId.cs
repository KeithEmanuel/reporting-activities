using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Defaults: Set Default TransactionId")]
    [Description("Sets the default TransactionId, which will be used by 'Log Message' when the TransactionId is not explicitly set.")]
    public class SetDefaultTransactionId : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("The new default TransactionId, used by 'Log Message'.")]
        public InArgument<String> TransactionId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            MessageRepository.Instance.DefaultTransactionId = TransactionId.Get(context);
        }
    }
}
