using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1 {
    public class CurrencyInfoTools {
        public List<CurrencyInfo> CurrencyInfoList { get; }
        public CurrencyInfoTools(List<CurrencyInfo> list) {
            CurrencyInfoList = list;
        }

        public double Average() => Math.Round(CurrencyInfoList.Select(el => el.ExchangeRate).Average(), 4);
        public double Sum() => Math.Round(CurrencyInfoList.Select(el => el.ExchangeRate).Sum(), 4);
        public CurrencyInfo Min() => CurrencyInfoList.Min();
        public CurrencyInfo Max() => CurrencyInfoList.Max();
        public double StandardDeviation() => Math.Round(
            Math.Sqrt(CurrencyInfoList.Select(el => el.ExchangeRate)
                .Sum(el => Math.Pow(el - Average(), 2)) / (CurrencyInfoList.Count() - 1)), 6);
        public CurrencyInfo[][] LargestExchangeRatesDifferences() {
            var list = new List<CurrencyInfo[]>();
            var minDates = CurrencyInfoList.Where(el => el.ExchangeRate == Min().ExchangeRate);
            var maxDates = CurrencyInfoList.Where(el => el.ExchangeRate == Max().ExchangeRate);
            foreach(var minDate in minDates) {
                foreach(var maxDate in maxDates) {
                    list.Add(new CurrencyInfo[] { minDate, maxDate });
                }
            }
            return list.ToArray();
        }

    }
}
