namespace CalculateModule.BackTestStochRSI
{
    using Binance.Net.Enums;
    using Binance.Net.Interfaces;
    using BinanceModule.BackTestRSI;
    using CalculateModule.ConfigFiles.Classes;
    using CalculateModule.ConfigFiles;
    using CryptoExchange.Net.CommonObjects;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static CalculateModule.BackTestRSI.TypesContainer;

    internal class ChooseBestStrategy
    {
        public ChooseBestStrategy()
        {

            //InitServices initialService = new InitServices();
            //IDbService dbService = initialService.GetServices().GetRequiredService<IDbService>();
            //IBinanceService binanceService = initialService.GetServices().GetRequiredService<IBinanceService>();
            //binanceService.AuthBinanceService("APIKEY", "APISECRET");
        }


        public BacktestResult<StochRSIConfig> StochRSI(ref List<IBinanceKline> klines, KlineInterval interval, string symbol = "LINAUSDT", int period = 14, int smooth = 4, decimal stopLoss = 0.10M, decimal takeProfit = 0.10M, decimal balance = 500)
        {
                BackTest<StochRSIConfig> stochRSIResult = new BackTest<StochRSIConfig>(
                    ref klines,
                    new StochRSIConfig
                    {
                        ShowConsole = true,
                        BackTestType = BackTestType.StochRSI,
                        Interval = interval,
                        Period = period,
                        Smooth = smooth,
                        StopLoss = stopLoss,
                        TakeProfit = takeProfit,
                        TransactionCosts = 0.01M,
                        Balance = balance
                    });

                BacktestResult<StochRSIConfig> stochRSI = stochRSIResult.GetStochRSI();
                return stochRSI;
       
        }
    }
}
