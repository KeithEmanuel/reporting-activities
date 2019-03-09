using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Defaults: Clear Default Tags")]
    [Description("Removes any defaults tags, so that new messages are log with no tags, if the Tags property is not set.")]
    public class ClearDefaultTags : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            MessageRepository.Instance.DefaultTags.Clear();
        }
    }
}
