﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace BLL
{
    public static class ConverterCurrency
    {
        public static IList<CurrencyInfo> ListCurrencyInfos
        {
            get { return GetCurrencyInfos(); }
        }

        public static double Convert(string charCode, int value)
        {
            if (charCode == "BYR")
                return value;
            var currency = ListCurrencyInfos.FirstOrDefault(item => item.CharCode == charCode);
            return currency.Rate * value;
        }

        private static IList<CurrencyInfo> GetCurrencyInfos()
        {
            XDocument xDoc = XDocument.Load("http://www.nbrb.by/Services/XmlExRates.aspx?ondate=" + DateTime.Now.ToString("MM/dd/yyyy"));
            var list = new List<CurrencyInfo>();
            foreach (var item in xDoc.Elements().First().Elements())
            {
                CurrencyInfo currencyInfo = new CurrencyInfo();
                var charCode = item.Element("CharCode");
                if (charCode != null)
                    currencyInfo.CharCode = charCode.Value;
                var rate = item.Element("Rate");
                if (rate != null)
                    currencyInfo.Rate = double.Parse(rate.Value, CultureInfo.InvariantCulture);
                list.Add(currencyInfo);
            }
            return list;
        }
    }
}
