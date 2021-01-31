using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelektika.Backend
{
    public class Record
    {
        public string name { get; set; }
        public int value1 { get; set; }
        public int value2 { get; set; }
        public string recordClass { get; set; }
        public string category { get; set; }

        public Record(string name, int value1, int value2,string cat, string recordClass)
        {
            this.category = cat;
            this.name = name;
            this.value1 = value1;
            this.value2 = value2;
            this.recordClass = recordClass;
        }
    }
}
