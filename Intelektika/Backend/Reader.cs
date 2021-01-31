using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelektika.Backend
{
    public static class Reader //taip pat naudojamas kaip global duomenims laikyti
    {
        public static string file;
        public static int kmeans;
        public static List<string> output = new List<string>();
        public static List<Record> records = new List<Record>();
        public static List<Record> toClasify = new List<Record>();
        

        public static void Read(bool clasify)
        {
            string[] lines = System.IO.File.ReadAllLines(file);
            if(clasify)
            {
                toClasify = new List<Record>();
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');
                    toClasify.Add(new Record(values[0], Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), Convert.ToString(values[3]), Convert.ToString(values[4])));
                }
            }
            else
            {
                records = new List<Record>();
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');
                    records.Add(new Record(values[0], Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), Convert.ToString(values[3]),Convert.ToString(values[4])));
                }
            }
        }
    }
}
