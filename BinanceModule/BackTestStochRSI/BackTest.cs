using Binance.Net.Interfaces;
using CalculateModule.BackTestRSI;
using CalculateModule.ConfigFiles;
using CalculateModule.ConfigFiles.Classes;
using Skender.Stock.Indicators;

namespace BinanceModule.BackTestRSI
{

    public class BackTest<T> where T : class
    {
        private List<IBinanceKline> Klines { get; set; }

        public T configuration;

        public BackTest(ref List<IBinanceKline> klines, T backTestConfig)
        {
            this.Klines = klines;
            this.configuration = backTestConfig;

            if (this.configuration is StochRSIConfig configStochRSI)
            {
                configStochRSI.KlinesLength = this.Klines.Count();
                configStochRSI.BalanceStart = configStochRSI.Balance;
            }
        }

        public BacktestResult<StochRSIConfig> GetStochRSI()
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

                BacktestResult<StochRSIConfig> backtestResult = backtester.Run(
                    this.Klines.Select(x => (double)x.ClosePrice).ToArray(),
                    configStochRSI.SellSignalStochRSI,
                    configStochRSI.BuySignalStochRSI,
                    this.Klines.Select(x => x.CloseTime.AddHours(2)).ToArray()
                    );

                backtestResult.Configuration = configStochRSI;
                return backtestResult;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

    }
}
