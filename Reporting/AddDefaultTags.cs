using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Defaults: Add Default Tags")]
    [Description("Adds one or more tags to the set of default tags.")]
    public class AddDefaultTags : CodeActivity
    {
        [Category("Input")]
        [Description("Add a single tag. This will be ignored if the Tags property is set.")]
        public InArgument<string> Tag { get; set; }

        [Category("Input")]
        [Description("Add multiple tags.")]
        public InArgument<string[]> Tags { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var tag = Tag.Get(context);
            var tags = Tags.Get(context);

            if(tags == null)
            {
                if(tag == null)
                {
                    return;
                }

                tags = new[] { tag };
            }

            MessageRepository.Instance.DefaultTags.AddRange(tags.Distinct());
        }
    }
}
