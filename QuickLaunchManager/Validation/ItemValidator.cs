using QuickLaunchManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLaunchManager.Validation
{
    public class ItemValidator : IItemValidator
    {
        public bool Validate(QuickLaunchItem item)
        {
            return !string.IsNullOrWhiteSpace(item.DisplayName)
                && !string.IsNullOrWhiteSpace(item.URI)
                && !string.IsNullOrWhiteSpace(item.Handler);
        }
    }
}
