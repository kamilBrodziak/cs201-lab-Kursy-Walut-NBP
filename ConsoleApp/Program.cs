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
            DateTime ConvertToDate(string s) {
                var parts = s.Split("-");
                if(parts.Length != 3) throw new ArgumentException("Incorrect date format");
                try {
                    var intParts = parts.Select(el => int.Parse(el)).ToArray();
                    return new DateTime(intParts[0], intParts[1], intParts[2]);
                } catch (Exception) {
                    throw new ArgumentException("Bad date format");
                }
            }
            if(args.Length != 3 || args[0].Length != 3)
                throw new ArgumentException("Incorrect command.");
            //var code = args[0].ToUpper();
            //var startDate = ConvertToDate(args[1]);
            //var endDate = ConvertToDate(args[2]);
            var startDate = new DateTime(2018, 9, 1);
            var endDate = new DateTime(2018, 9, 20);
            if(startDate > DateTime.Now)
                throw new ArgumentException("Start date must be in the past.");
            if(endDate < startDate)
                throw new ArgumentException("End date must be later than start date.");
            if(startDate.Year < 2002)
                throw new ArgumentException("There is no records before 2002.");
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
