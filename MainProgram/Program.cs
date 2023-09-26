using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using MainProgram.ServicesComponent;
using BinanceModule.BackTestRSI;
using Infrastructure.Services.BinanceService;
using Infrastructure.Services;
using CalculateModule.ConfigFiles;
using CalculateModule.ConfigFiles.Classes;
using Binance.Net.Interfaces;
using Binance.Net.Enums;
using static CalculateModule.BackTestRSI.TypesContainer;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();

        InitServices initialService = new InitServices();
        IDbService dbService = initialService.GetServices().GetRequiredService<IDbService>();
        IBinanceService binanceService = initialService.GetServices().GetRequiredService<IBinanceService>();
        binanceService.AuthBinanceService("APIKEY", "APISECRET");


        List<BacktestResult<StochRSIConfig>> ListBackTestResult = await RunBacktest(binanceService);


        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        watch.Stop();


        foreach (var stochRSI in ListBackTestResult.OrderByDescending(x => x.TotalProfitLossLast).ToList())
        {
            Console.WriteLine(
                $"({stochRSI.Configuration.Period} {stochRSI.Configuration.Smooth}) " +
                $"({stochRSI.Configuration.TakeProfit} {stochRSI.Configuration.StopLoss}) " +
                $"({stochRSI.Configuration.BuySignalStochRSI} {stochRSI.Configuration.SellSignalStochRSI}) " +
                $"{stochRSI.Configuration.Interval.ToString()} " +
                $" {Math.Round(stochRSI.TotalProfitLossLast, 2)} " +
                $" \n NumberOfTrades:{stochRSI.NumberOfTrades} NumberOfWinningTrades:{stochRSI.NumberOfWinningTrades} NumberOfLosingTrades:{stochRSI.NumberOfLosingTrades}");
        }



        //to do - bulk insert
        //await dbService.SaveStrategy(PrepareData.ObjectPrepareStochRSI(ref stochRSIResult, ref stochRSI));

        Console.ReadLine();
    }

   



    static async Task<List<BacktestResult<StochRSIConfig>>> RunBacktest(IBinanceService binanceService)
    {
        string symbol = "IDUSDT";
        DateTime startDate = DateTime.ParseExact("2023-09-01 09:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.Now;

        KlineInterval[] intervals = (KlineInterval[])Enum.GetValues(typeof(KlineInterval));
        int startInterval = 2; // FiveMinutes
        int endInterval = 8;

        List<BacktestResult<StochRSIConfig>> ListBackTestResult = new List<BacktestResult<StochRSIConfig>>();

        for (int i = startInterval; i <= endInterval; i++)
        {
            List<IBinanceKline> klinesBatchedData = await binanceService.GetKlinesBatchedAsync(
                symbol,
                intervals[i],
                startDate,
                endDate);

            ListBackTestResult.AddRange(RunBacktestForParameters(klinesBatchedData, intervals[i]));
        }

        return ListBackTestResult;
    }

    static List<BacktestResult<StochRSIConfig>> RunBacktestForParameters(List<IBinanceKline> klinesBatchedData, KlineInterval interval)
    {
        List<BacktestResult<StochRSIConfig>> results = new List<BacktestResult<StochRSIConfig>>();

        for (int period = 14; period <= 14; period++)
        {
            for (int smooth = 4; smooth <= 4; smooth++)
            {
                for (decimal jstepProfit = 0.17M; jstepProfit <= 0.20M; jstepProfit += 0.04M)
                {
                    for (decimal kstepLoss = 0.13M; kstepLoss <= 0.20M; kstepLoss += 0.04M)
                    {
                        results.AddRange(RunBacktestForConfig(klinesBatchedData, interval, period, smooth, jstepProfit, kstepLoss));
                    }
                }
            }
        }

        return results;
    }

    static List<BacktestResult<StochRSIConfig>> RunBacktestForConfig(List<IBinanceKline> klinesBatchedData, KlineInterval interval, int period, int smooth, decimal jstepProfit, decimal kstepLoss)
    {
        List<BacktestResult<StochRSIConfig>> results = new List<BacktestResult<StochRSIConfig>>();

        for (int buySignal = 0; buySignal <= 6; buySignal += 1)
        {
            for (int sellSignal = 90; sellSignal <= 100; sellSignal += 5)
            {
                BackTest<StochRSIConfig> stochRSIResult = new BackTest<StochRSIConfig>(
                    ref klinesBatchedData,
                    new StochRSIConfig
                    {
                        ShowConsole = false,
                        Balance = 500,
                        BackTestType = BackTestType.StochRSI,
                        Interval = interval,
                        Period = period,
                        Smooth = smooth,
                        StopLoss = kstepLoss,
                        TakeProfit = jstepProfit,
                        TransactionCosts = 0.01M,
                        BuySignalStochRSI = buySignal,
                        SellSignalStochRSI = sellSignal,
                    });

                BacktestResult<StochRSIConfig> stochRSI = stochRSIResult.GetStochRSI();
                lock (results)
                {
                    if (results.Count() >= 100)//Optymalizacja pamieci dla slabych wynikow
                    {
                        if (results.Last().TotalProfitLossLast < stochRSI.TotalProfitLossLast)
                        {
                            results.RemoveAt(results.Count() - 1);
                            results.Add(stochRSI);
                        }
                    }
                    else
                    {
                        results.Add(stochRSI);
                    }
                }
            }
        }

        return results;
    }
}






//TOP 10
