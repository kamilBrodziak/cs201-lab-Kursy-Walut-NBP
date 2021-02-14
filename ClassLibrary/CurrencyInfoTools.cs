using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary {
    public class CurrencyInfoTools {
        public List<CurrencyInfo> CurrencyInfoList { get; }
        public CurrencyInfoTools(List<CurrencyInfo> list) {
            CurrencyInfoList = list;
        }

        public double Average() => Math.Round(CurrencyInfoList.Select(el => el.ExchangeRate).Average(), 4);
        public double Sum() => Math.Round(CurrencyInfoList.Select(el => el.ExchangeRate).Sum(), 4);
        public double Min() => CurrencyInfoList.Select(el => el.ExchangeRate).Min();
        public double Max() => CurrencyInfoList.Select(el => el.ExchangeRate).Max();
        public CurrencyInfo[][] LargestExchangeRatesDifferences() {
            var list = new List<CurrencyInfo[]>();
            var minDates = CurrencyInfoList.Where(el => el.ExchangeRate == Min());
            var maxDates = CurrencyInfoList.Where(el => el.ExchangeRate == Max());
            foreach(var minDate in minDates) {
                foreach(var maxDate in maxDates) {
                    list.Add(new CurrencyInfo[] { minDate, maxDate });
                }
            }
            return list.ToArray();
        }

    }
}
