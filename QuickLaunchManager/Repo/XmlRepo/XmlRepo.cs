using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using QuickLaunchManager.Models;

namespace QuickLaunchManager.Repo.XmlRepo
{
    class XmlRepo : IRepo
    {
        private readonly XmlDb _storage;
        
        public XmlRepo(string fileName)
        {
            _storage = new XmlDb(fileName);
            _storage.Load();
        }
        

        public void DeleteItme(Guid id)
        {
            var item = _storage.items.FirstOrDefault(i => i.Id == id);
            if (item == null) throw new Exception("Item id not found");
            var items = _storage.items.Remove(item);
            Save();
        }

        public QuickLaunchItem GetItem(Guid id)
        {
            _storage.LoadIfChanged();
            return _storage.items.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<QuickLaunchItem> GetItems()
        {
            _storage.LoadIfChanged();
            return _storage.items.ToList();
        }

        
        public void Save()
        {
            _storage.Save();
        }

        public void AddItem(QuickLaunchItem item)
        {
            item.Id = item.Id ?? Guid.NewGuid();
            if (_storage.items.Any(i => i.Id == item.Id)) throw new Exception("Duplicate id");
            _storage.items.Add(item);
            
        }

        public void UpdateItem(QuickLaunchItem item)
        {
            var _item = _storage.items.FirstOrDefault(i => i.Id == item.Id);
            if (item == null) throw new Exception("Id doesn't exist");
            _storage.items.Remove(_item);
            _storage.items.Add(item);
        }
        
    }
}
