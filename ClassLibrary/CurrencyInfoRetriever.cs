using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ClassLibrary {
    public class CurrencyInfoRetriever {
        private const string BASE_URL = "https://www.nbp.pl/kursy/xml/";

        public static CurrencyInfo GetCurrencyInfoFromXML(XDocument doc, string code) {
            var currency = doc.Descendants("kod_waluty")
                    .Where(el => el.Value == code)
                    .First()
                    .Parent;
            var dateParts = doc.Descendants("data_publikacji").First().Value
                .Split('-').Select(el => int.Parse(el)).ToArray();
            return new CurrencyInfo(
                currency.Element("nazwa_waluty").Value,
                int.Parse(currency.Element("przelicznik").Value),
                currency.Element("kod_waluty").Value,
                double.Parse(currency.Element("kurs_sredni").Value.Replace(",", ".")),
                new DateTime(dateParts[0], dateParts[1], dateParts[2]));
        }

        public static CurrencyInfo GetCurrencyInfoFromFile(string code, string fileName) {
            var doc = XDocument.Load($"{BASE_URL}{fileName}.xml");
            return GetCurrencyInfoFromXML(doc, code);
        }

        public static List<CurrencyInfo> GetCurrencyInfoListFromFiles(string code, string[] fileNames) {
            var list = new List<CurrencyInfo>();
            foreach(var fileName in fileNames) {
                list.Add(GetCurrencyInfoFromFile(code, fileName));
            }
            return list;
        }

        public static string[] GetCurrencyXMLFilesNamesListFromFile(int year, DateTime startDate, DateTime endDate) {
            var yearStr = year != DateTime.Now.Year ? $"{year}" : "";
            return new WebClient().DownloadString($"{BASE_URL}dir{yearStr}.txt")
                .Split('\n')
                .Select(el => Regex.Replace(el, "[^a-zA-Z0-9]+", ""))
                .Where(el => {
                    var dateStr = el.Split('z')[^1];
                    var dateParts = Enumerable.Range(0,3)
                    .Select(i => int.Parse(dateStr.Substring(i * 2, 2))).ToArray();
                    var date = new DateTime(2000 + dateParts[0], dateParts[1], dateParts[2]);
                    return el.Length > 0 && el[0] == 'a' && date <= endDate && date >= startDate; 
                }).ToArray();
        }

        public static string[] GetCurrencyXMLFilesNamesListFromFile(DateTime startDate, DateTime endDate) {
            var list = new List<string>();
            for(int i = startDate.Year; i <= endDate.Year; i++)
                list.AddRange(GetCurrencyXMLFilesNamesListFromFile(i, startDate, endDate));
            return list.ToArray();
        }

    }
}
