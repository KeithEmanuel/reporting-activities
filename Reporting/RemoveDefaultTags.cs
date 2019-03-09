using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    [DisplayName("Defaults: Remove Default Tags")]
    [Description("Remove all specified tags from the default tags.")]
    public class RemoveDefaultTags : CodeActivity
    {

        [Category("Input")]
        [Description("The tag to remove.")]
        public InArgument<string> Tag { get; set; }

        [Category("Input")]
        [Description("The tags to remove.")]
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

            foreach(string t in tags)
            {
                MessageRepository.Instance.DefaultTags.Remove(t);
            }
        }
    }
}
