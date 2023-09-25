namespace CalculateModule.BackTestRSI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class TypesContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public enum BackTestType
        {
            StochRSI
        }
        /// <summary>
        /// 
        /// </summary>
        public enum PriceType
        {
            High,
            Close,
            Low,
            Open
        }
        /// <summary>
        /// 
        /// </summary>
        public enum SignalType
        {
            Buy,
            Sell,
            None,
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="klines"></param>
        /// <param name="priceType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<(DateTime, double)> GetPriceData(List<Binance.Net.Interfaces.IBinanceKline> klines, PriceType priceType)
        {
            switch (priceType)
            {
                case PriceType.High:
                    return klines.Select(kline => (kline.OpenTime.AddHours(2), (double)kline.HighPrice));
                case PriceType.Close:
                    return klines.Select(kline => (kline.OpenTime.AddHours(2), (double)kline.ClosePrice));
                case PriceType.Low:
                    return klines.Select(kline => (kline.OpenTime.AddHours(2), (double)kline.LowPrice));
                case PriceType.Open:
                    return klines.Select(kline => (kline.OpenTime.AddHours(2), (double)kline.OpenPrice));
                default:
                    throw new ArgumentException("Invalid PriceType.");
            }
        }
    }
}
