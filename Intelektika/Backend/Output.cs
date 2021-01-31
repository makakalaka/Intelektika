using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelektika.Backend
{
    public class Output
    {
        public Record rec;
        public double ratio;
        public string cla;

        public Output(Record r, double ra)
        {
            cla = r.recordClass;
            rec = r;
            ratio = ra;
        }
    }
}
