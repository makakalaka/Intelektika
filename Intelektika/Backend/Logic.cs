using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelektika.Backend
{
    public static class Logic
    {
        public static void Calculate()
        {
            Reader.output.Clear();
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Euclidean();
            watch.Stop();
            Reader.output.Add("::Time taken::"+ watch.ElapsedMilliseconds+" miliseconds.");
            Gap();
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Manhattan();
            watch.Stop();
            Reader.output.Add("::Time taken::" + watch.ElapsedMilliseconds + " miliseconds.");
        }

        public static void Euclidean()
        {
            Reader.output.Add("--------Euclidean--------");
            foreach (Record clas in Reader.toClasify)//kiekvienam ieskomam objektui
            {
                Reader.output.Add("'To clasify': "+clas.name);
                List<Output> ratios = new List<Output>();
                foreach (Record rec in Reader.records)//skaiciuojamos visos 'records' reiksmes su ieskomu elementu
                {
                    if (clas.category == rec.category)
                    {
                        double ratio = Math.Sqrt(Math.Pow((rec.value1 - clas.value1), 2) + Math.Pow((rec.value2 - clas.value2), 2));//euklido formule
                        ratios.Add(new Output(new Record(rec.name, rec.value1, rec.value2, rec.category, rec.recordClass), ratio));//saugomi ratios

                    }
                }
                CalcK(ratios, clas.name);
            }
        }

        public static void Manhattan()
        {
            Reader.output.Add("--------Manhattan--------");
            foreach (Record clas in Reader.toClasify)//kiekvienam ieskomam objektui
            {
                Reader.output.Add("'To clasify': " + clas.name);
                List<Output> ratios = new List<Output>();
                foreach (Record rec in Reader.records)//skaiciuojamos visos 'records' reiksmes su ieskomu elementu
                {
                    if (clas.category == rec.category)
                    {
                        double ratio = Math.Abs(Math.Pow((rec.value1 - clas.value1), 2) + Math.Pow((rec.value2 - clas.value2), 2));//euklido formule
                        ratios.Add(new Output(new Record(rec.name, rec.value1, rec.value2, rec.category, rec.recordClass), ratio));//saugomi ratios

                    }
                }
                CalcK(ratios, clas.name);
            }
        }

        private static void CalcK(List<Output> ratios,string className)
        {
            int kmeans = Reader.kmeans;
            if (kmeans > ratios.Count)
                kmeans = ratios.Count;
            List<Output> sortedList = ratios.OrderBy(o => o.ratio).ToList();//sukuriamas naujas listas su obj+ratio
            foreach (Output o in sortedList)//output generacija
                Reader.output.Add(className + " -> " + o.rec.name + " class: " + o.rec.recordClass+ " ratio: " + o.ratio);
            List<string> distinctClasses = sortedList.Select(x => x.cla).Distinct().ToList(); //distinct klases
            List<int> hits = new List<int>(); //kiek kartu buvo parinkta klase
            foreach (string s in distinctClasses)//sukuriamas list size=distinct classes
                hits.Add(0);
            for (int j = 0; j < distinctClasses.Count; j++)//skaiciuojama kiek kartu priskirta kokiai klasei
            {
                for (int i = 0; i < kmeans; i++)
                {
                    if (sortedList[i].cla == distinctClasses[j])
                        hits[j] = hits[j] + 1;
                }
            }
            int maxClass = hits.Max(t => t);//randama daugiausiai hits surinkusi klase
            Console.WriteLine("-------------" + maxClass);
            int dupe = 0;
            foreach (int s in hits)//tikrinama, ar tai tik vienintele klase turinti max
                if (s == maxClass)
                    dupe++;
            string winnerClass="error in winner class assign";
            for (int i = 0; i < distinctClasses.Count; i++)
                if (hits[i] == maxClass)
                    winnerClass = distinctClasses[i];
            if(dupe>1)
                Reader.output.Add("::Classified:: unknown");
            else
            {
                Reader.output.Add("::Classified:: "+winnerClass);
            }
        }
        private static void Gap()
        {
            Reader.output.Add("");
            Reader.output.Add("");
        }
    }
}
