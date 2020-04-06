using System;
using System.Threading.Tasks;

namespace MI.CsvParser.Example
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CsvParser<ProductAttribute, ProductAttributeMap>.ParseAsync("Documents\\ProductsList.csv").ConfigureAwait(false);

            Console.WriteLine("Hello World!");
        }
    }
}
