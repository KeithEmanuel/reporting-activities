using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Truncate Messages")]
    [Description("Removes all messages from the logs.")]
    public class TruncateMessages : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            MessageRepository.Instance.Messages.Clear();
        }
    }
}
