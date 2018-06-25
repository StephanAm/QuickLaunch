using QuickLaunchManager.Models;

namespace QuickLaunchManager.Validation
{
    public interface IItemValidator
    {
        bool Validate(QuickLaunchItem item);
    }
}