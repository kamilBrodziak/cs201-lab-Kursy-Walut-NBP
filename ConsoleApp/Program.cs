using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using ClassLibrary1;
namespace ConsoleApp {
    class Program {
        static void Main(string[] args) {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //var startDateArr = args[1].Split("-").Select(el => int.Parse(el)).ToArray();
            //var endDateArr = args[2].Split("-").Select(el => int.Parse(el)).ToArray();

            //var startDate = new DateTime(startDateArr[0], startDateArr[1], startDateArr[2]);
            //var endDate = new DateTime(endDateArr[0], endDateArr[1], endDateArr[2]);
            //var code = args[0];
            var startDate = new DateTime(2018, 9, 1);
            var endDate = new DateTime(2018, 9, 20);
            //var code = args[0];
            var code = "EUR";
            var currencyInfoTools = new CurrencyInfoTools(
                CurrencyInfoRetriever.GetCurrencyInfoListFromFiles(code,
                CurrencyInfoRetriever.GetCurrencyXMLFilesNamesListFromFile(startDate, endDate)));
            Console.WriteLine($"Average rate: {currencyInfoTools.Average()}");
            Console.WriteLine($"Standard deviation: {currencyInfoTools.StandardDeviation()}");
            Console.WriteLine($"Minimum rate: ${currencyInfoTools.Min().ToString(CurrencyInfo.DisplayOptions.ExchangeRate, CurrencyInfo.DisplayOptions.Date)}");
            Console.WriteLine($"Minimum rate: ${currencyInfoTools.Max().ToString(CurrencyInfo.DisplayOptions.ExchangeRate, CurrencyInfo.DisplayOptions.Date)}");
            foreach(var currPair in currencyInfoTools.LargestExchangeRatesDifferences()) {
                Console.WriteLine($"Largest difference: {Math.Abs(currPair.First() - currPair.Last())}"+
                    $" ({currPair.First().ToString(CurrencyInfo.DisplayOptions.ExchangeRate, CurrencyInfo.DisplayOptions.Date)}" + 
                    $" - {currPair.Last().ToString(CurrencyInfo.DisplayOptions.ExchangeRate, CurrencyInfo.DisplayOptions.Date)})"
                    );
            }
        }
    }
}
