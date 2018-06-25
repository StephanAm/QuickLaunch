using QuickLaunchManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace QuickLaunchManager.Repo.XmlRepo
{
    public class XmlDb
    {
        private DateTime _lastLoad;
        private readonly string _fileName;
        public List<QuickLaunchItem> items { get; set; }

        public XmlDb() { }
        public XmlDb(string fileName)
        {
            _fileName = fileName;
            items = new List<QuickLaunchItem>();
        }
        public void Save()
        {
            using (var file = File.OpenWrite(_fileName))
            {
                new XmlSerializer(typeof(XmlDb)).Serialize(file, this);
            }
        }
        public void LoadIfChanged()
        {
            if (_lastLoad < File.GetLastWriteTime(_fileName))
            {
                Load();
            }
        }
        public void Load()
        {
            var fileName = _fileName;
            if (File.Exists(fileName))
            {
                using (var file = File.OpenRead(fileName))
                {
                    var loadedDb = new XmlSerializer(typeof(XmlDb)).Deserialize(file) as XmlDb;
                    this.items.Clear();
                    this.items.AddRange(loadedDb.items);
                }
                _lastLoad = DateTime.Now;
            }
        }

    }
}
