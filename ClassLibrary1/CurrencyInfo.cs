using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1 {
    public class CurrencyInfo : IComparable<CurrencyInfo> {
        public string Name { get; private set; }
        public int Converter { get; private set; }
        public string Code { get; private set; }
        public double ExchangeRate { get; private set; }
        public DateTime Date { get; private set; }
        public CurrencyInfo(string name, int converter, string code, double rate, DateTime date) {
            Name = name;
            Converter = converter;
            Code = code;
            ExchangeRate = rate;
            Date = date;
        }

        public enum DisplayOptions {
            Name, Converter, Code, ExchangeRate, Date
        }

        public string ToString(params DisplayOptions[] displayOptions) {
            var arr = new string[] { "", "", "", "", "" };
            foreach(var option in displayOptions) {
                switch(option) {
                    case DisplayOptions.Name:
                        arr[0] = $"Name: {Name}";
                        break;
                    case DisplayOptions.Code:
                        arr[1] = $"Code: {Code}";
                        break;
                    case DisplayOptions.Date:
                        arr[2] = $"Date: {Date:yyyy-MM-dd}";
                        break;
                    case DisplayOptions.ExchangeRate:
                        arr[3] = $"Exchange rate: {ExchangeRate}";
                        break;
                    case DisplayOptions.Converter:
                        arr[4] = $"Converter: {Converter}";
                        break;
                }
            }
            return string.Join(", ", arr.Where(el => el != ""));
        }

        public int CompareTo(CurrencyInfo obj) => this.ExchangeRate > obj.ExchangeRate ? 1 :
            (this.ExchangeRate < obj.ExchangeRate ? -1 : 0);

        public static double operator -(CurrencyInfo first, CurrencyInfo sec) 
            => Math.Round(first.ExchangeRate - sec.ExchangeRate, 4);
    }
}
