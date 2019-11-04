using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace ManhattanLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                DataParser();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Exit Program");
            }
        }

        static void DataParser()
        {
            string alldata = File.ReadAllText("../../../../data.json");
            Example manhattan = JsonConvert.DeserializeObject<Example>(alldata);

            var all = from mht in manhattan.features
                      select mht.properties.neighborhood;

            foreach(var nbh in all)
            {
                Console.WriteLine(nbh);
            }


            Console.WriteLine();
            Console.WriteLine("Neighborhood names");

            var filterData = from hood in all
                             where hood.Length > 0
                             select hood;
            foreach(var nbh in filterData)
            {
                Console.WriteLine(nbh);
            }


            Console.WriteLine();
            Console.WriteLine("Neighborhoods without duplicates");

            Console.WriteLine("All neighborhoods from all queries.");
            var allQueries = manhattan.features.Where(a => a.properties.neighborhood.Length > 0)
                .GroupBy(b => b.properties.neighborhood)
                .Select(c => c.First());
            foreach (var nbh in allQueries)
            {
                Console.WriteLine(nbh.properties.neighborhood);
            }


            Console.WriteLine();
            Console.WriteLine("Revised Neighborhoods");

            var newQuery = manhattan.features.Select(d => d.properties.neighborhood);
            foreach(var nbh in newQuery)
            {
                Console.WriteLine(nbh);
            }

        }
    }
}
