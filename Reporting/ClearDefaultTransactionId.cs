using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Defaults: Clear Default TransactionId")]
    [Description("Clears the default TransactionId, so that new messages are logged with no default TransactionId.")]
    public class ClearDefaultTransactionId : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            MessageRepository.Instance.DefaultTransactionId = null;
        }
    }
}
