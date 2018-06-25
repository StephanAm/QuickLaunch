using QuickLaunchManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLaunchManager.Repo
{
    public interface IRepo
    {
        QuickLaunchItem GetItem(Guid id);
        IEnumerable<QuickLaunchItem> GetItems();
        void DeleteItme(Guid id);
        void AddItem(QuickLaunchItem item);
        void UpdateItem(QuickLaunchItem item);
        void Save();
    }
}
