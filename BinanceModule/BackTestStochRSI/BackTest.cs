using CalculateModule.BackTestRSI;
using CalculateModule.ConfigFiles;
using CalculateModule.ConfigFiles.Classes;
using Skender.Stock.Indicators;
using static CalculateModule.BackTestRSI.TypesContainer;

namespace BinanceModule.BackTestRSI
{

    public class BackTest<T> where T : class
    {
        private List<Binance.Net.Interfaces.IBinanceKline> Klines { get; set; }

        public T configuration;

        public BackTest(ref List<Binance.Net.Interfaces.IBinanceKline> klines, T backTestConfig)
        {
            this.Klines = klines;
            this.configuration = backTestConfig;
            if (this.configuration is StochRSIConfig configStochRSI)
            {
                configStochRSI.KlinesLength = this.Klines.Count();
                configStochRSI.BalanceStart = configStochRSI.Balance;
            }
        }

        public BacktestResult GetStochRSI()
        {
            if (this.configuration is StochRSIConfig configStochRSI)
            {

                var stochRsiClosePrice = Indicator.GetStochRsi(
                    TypesContainer.GetPriceData(this.Klines, TypesContainer.PriceType.Close),
                    configStochRSI.Period,
                    configStochRSI.Period,
                    configStochRSI.Smooth,
                    configStochRSI.Smooth)
                     .ToList();

                StochRSIBackTester backtester = new StochRSIBackTester(
                    configStochRSI.StopLoss,
                    configStochRSI.TakeProfit,
                    configStochRSI.TransactionCosts,
                    stochRsiClosePrice,
                    configStochRSI);

                BacktestResult backtestResult = backtester.Run(
                    this.Klines.Select(x => (double)x.ClosePrice).ToArray(),
                    configStochRSI.SellStochRSI,
                    configStochRSI.BuyStochRSI,
                    this.Klines.Select(x => x.CloseTime.AddHours(2)).ToArray()
                    );

                return backtestResult;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

    }
}
