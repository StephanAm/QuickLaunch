using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLaunchManager.Models
{

    public class QuickLaunchItem
    {
        public Guid? Id {get;set;}
        public string DisplayName { get; set; }
        public string URI { get; set; }
        public string Handler { get; set; }
        public string Group { get; set; }
    }
}
