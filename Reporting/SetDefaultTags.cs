using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Defaults: Set Default Tags")]
    [Description("Sets the default tags for any logged messages. This removes any other tags already set.")]
    public class SetDefaultTags : CodeActivity
    {
        [Category("Input")]
        [Description("The tag to set. This will remove all other tags. Ignored if the Tags property is set.")]
        public InArgument<string> Tag { get; set; }

        [Category("Input")]
        [Description("The tags to set. This will remove all other tags. Setting this property will ignore the Tag property.")]
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

            MessageRepository.Instance.DefaultTags = tags.ToList();
        }
    }
}
