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


        InitServices initialService = new();
        IDbService dbService = initialService.GetServices().GetRequiredService<IDbService>();
        IBinanceService binanceService = initialService.GetServices().GetRequiredService<IBinanceService>();
        binanceService.AuthBinanceService("APIKEY", "APISECRET");

        Config config = new()
        {
            Symbol = "IDUSDT",
            ShowConsole = false,
            Balance = 1000,
            BackTestType = BackTestType.StochRSI,
            TransactionCosts = 0.01M,
            StartDate = DateTime.ParseExact("2023-09-01 09:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
            EndDate = DateTime.Now
        };

        //var watchSync = new System.Diagnostics.Stopwatch(); watchSync.Start();
        //List<BacktestResult<StochRSIConfig>>? StochRSIResultSync = await RunBackTestSync(binanceService, config); //~~59 sec
        //watchSync.Stop();
        //Console.WriteLine($"Execution Time: {watchSync.ElapsedMilliseconds} ms");

        //if (StochRSIResultSync != null)
        //{
        //    foreach (var stochRSI in StochRSIResultSync.OrderByDescending(x => x.TotalProfitLossLast).ToList())
        //    {
        //        Console.WriteLine(
        //            $"({stochRSI.Configuration?.Period} {stochRSI.Configuration?.Smooth}) " +
        //            $"({stochRSI.Configuration?.TakeProfit} {stochRSI.Configuration?.StopLoss}) " +
        //            $"({stochRSI.Configuration?.BuySignalStochRSI} {stochRSI.Configuration?.SellSignalStochRSI}) " +
        //            $"{stochRSI.Configuration?.Interval.ToString()} " +
        //            $" {Math.Round(stochRSI.TotalProfitLossLast, 2)} $ " +
        //            $" \n NumberOfTrades:{stochRSI.NumberOfTrades} " +
        //            $"    NumberOfWinningTrades:{stochRSI.NumberOfWinningTrades} " +
        //            $"    NumberOfLosingTrades:{stochRSI.NumberOfLosingTrades} ");
        //    }
        //}

        //var watchAsync = new System.Diagnostics.Stopwatch(); watchAsync.Start();
        //HashSet<BacktestResult<StochRSIConfig>>? StochRSIResultAsync = await RunBackTestAsync(binanceService, config); //~~33 sec
        //watchAsync.Stop();
        //Console.WriteLine($"Execution Time: {watchAsync.ElapsedMilliseconds} ms");


        //if (StochRSIResultAsync != null)
        //{
        //    foreach (var stochRSI in StochRSIResultAsync.OrderByDescending(x => x.TotalProfitLossLast).ToList())
        //    {
        //        Console.WriteLine(
        //            $"({stochRSI.Configuration?.Period} {stochRSI.Configuration?.Smooth}) " +
        //            $"({stochRSI.Configuration?.TakeProfit} {stochRSI.Configuration?.StopLoss}) " +
        //            $"({stochRSI.Configuration?.BuySignalStochRSI} {stochRSI.Configuration?.SellSignalStochRSI}) " +
        //            $"{stochRSI.Configuration?.Interval.ToString()} " +
        //            $" {Math.Round(stochRSI.TotalProfitLossLast, 2)} $ " +
        //            $" \n NumberOfTrades:{stochRSI.NumberOfTrades} " +
        //            $"    NumberOfWinningTrades:{stochRSI.NumberOfWinningTrades} " +
        //            $"    NumberOfLosingTrades:{stochRSI.NumberOfLosingTrades} ");
        //    }
        //}



        //var watchx = new System.Diagnostics.Stopwatch(); watchx.Start();
        //List<BacktestResult<StochRSIConfig>> results = new List<BacktestResult<StochRSIConfig>>();
        //KlineInterval[] intervals = (KlineInterval[])Enum.GetValues(typeof(KlineInterval));
        //int startInterval = 2; // FiveMinutes
        //int endInterval = 8;

        //List<BacktestResult<StochRSIConfig>> ListBackTestResult = new List<BacktestResult<StochRSIConfig>>();

        //for (int i = startInterval; i <= endInterval; i++)
        //{
        //    List<IBinanceKline> klinesBatchedData = await binanceService.GetKlinesBatchedAsync(
        //    config.Symbol,
        //    intervals[i],
        //    config.StartDate,
        //    config.EndDate);

        //    for (int period = 14; period <= 14; period++)
        //    {
        //        for (int smooth = 4; smooth <= 4; smooth++)
        //        {
        //            for (decimal jstepProfit = 0.17M; jstepProfit <= 0.20M; jstepProfit += 0.04M)
        //            {
        //                for (decimal kstepLoss = 0.13M; kstepLoss <= 0.20M; kstepLoss += 0.04M)
        //                {
        //                    for (int buySignal = 1; buySignal <= 1; buySignal += 1)
        //                    {
        //                        for (int sellSignal = 100; sellSignal <= 100; sellSignal += 5)
        //                        {
        //                            BackTest<StochRSIConfig> stochRSIResult = new BackTest<StochRSIConfig>(
        //                                ref klinesBatchedData,
        //                                new StochRSIConfig
        //                                {
        //                                    Symbol = config.Symbol,
        //                                    ShowConsole = config.ShowConsole,
        //                                    Balance = config.Balance,
        //                                    TransactionCosts = config.TransactionCosts,
        //                                    BackTestType = config.BackTestType,
        //                                    Interval = intervals[i],
        //                                    Period = period,
        //                                    Smooth = smooth,
        //                                    StopLoss = kstepLoss,
        //                                    TakeProfit = jstepProfit,
        //                                    BuySignalStochRSI = buySignal,
        //                                    SellSignalStochRSI = sellSignal,
        //                                });

        //                            BacktestResult<StochRSIConfig> stochRSI = stochRSIResult.GetStochRSI();
        //                            lock (results)
        //                            {
        //                                if (results.Count() >= 3)//Optymalizacja pamieci dla slabych wynikow
        //                                {
        //                                    if (results.Last().TotalProfitLossLast < stochRSI.TotalProfitLossLast)
        //                                    {
        //                                        results.RemoveAt(results.Count() - 1);
        //                                        results.Add(stochRSI);
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    results.Add(stochRSI);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //watchx.Stop();
        //Console.WriteLine($"Execution Time: {watchx.ElapsedMilliseconds} ms");
        //if (results != null)
        //{
        //    foreach (var stochRSI in results.OrderByDescending(x => x.TotalProfitLossLast).ToList())
        //    {
        //        Console.WriteLine(
        //            $"({stochRSI.Configuration?.Period} {stochRSI.Configuration?.Smooth}) " +
        //            $"({stochRSI.Configuration?.TakeProfit} {stochRSI.Configuration?.StopLoss}) " +
        //            $"({stochRSI.Configuration?.BuySignalStochRSI} {stochRSI.Configuration?.SellSignalStochRSI}) " +
        //            $"{stochRSI.Configuration?.Interval.ToString()} " +
        //            $" {Math.Round(stochRSI.TotalProfitLossLast, 2)} $ " +
        //            $" \n NumberOfTrades:{stochRSI.NumberOfTrades} " +
        //            $"    NumberOfWinningTrades:{stochRSI.NumberOfWinningTrades} " +
        //            $"    NumberOfLosingTrades:{stochRSI.NumberOfLosingTrades} ");
        //    }
        //}




        //to do - bulk insert
        //await dbService.SaveStrategy(PrepareData.ObjectPrepareStochRSI(ref stochRSIResult, ref stochRSI));

        Console.ReadLine();
    }





    static async Task<HashSet<BacktestResult<StochRSIConfig>>?> RunBackTestAsync(IBinanceService binanceService, Config config)
    {
        if (config.BackTestType == BackTestType.StochRSI)
        {
            KlineInterval[] intervals = (KlineInterval[])Enum.GetValues(typeof(KlineInterval));
            int startInterval = 2; // FiveMinutes
            int endInterval = 8;

            HashSet<BacktestResult<StochRSIConfig>> ListBackTestResult = new HashSet<BacktestResult<StochRSIConfig>>();

            await Task.WhenAll(
                Enumerable.Range(startInterval, endInterval - startInterval + 1).Select(async i =>
                {
                    List<IBinanceKline> klinesBatchedData = await binanceService.GetKlinesBatchedAsync(
                        config.Symbol,
                        intervals[i],
                        config.StartDate,
                        config.EndDate);

                    ListBackTestResult.UnionWith(await RunBacktestForParametersAsync(klinesBatchedData, intervals[i], config));
                })
            );
            return ListBackTestResult;
        }

        return null;
    }

    static async Task<List<BacktestResult<StochRSIConfig>>> RunBacktestForParametersAsync(List<IBinanceKline> klinesBatchedData, KlineInterval interval, Config config)
    {
        List<BacktestResult<StochRSIConfig>> results = new List<BacktestResult<StochRSIConfig>>();

        await Task.WhenAll(
            Enumerable.Range(14, 1).Select(async period =>
            {
                await Task.WhenAll(
                    Enumerable.Range(4, 1).Select(async smooth =>
                    {
                        for (decimal jstepProfit = 0.17M; jstepProfit <= 0.20M; jstepProfit += 0.04M)
                        {
                            for (decimal kstepLoss = 0.13M; kstepLoss <= 0.20M; kstepLoss += 0.04M)
                            {
                                results.AddRange(await RunBacktestForConfigAsync(klinesBatchedData, interval, period, smooth, jstepProfit, kstepLoss, config));
                            }
                        }
                    })
                );
            })
        );

        return results;
    }

    static async Task<List<BacktestResult<StochRSIConfig>>> RunBacktestForConfigAsync(List<IBinanceKline> klinesBatchedData, KlineInterval interval, int period, int smooth, decimal jstepProfit, decimal kstepLoss, Config config)
    {
        List<BacktestResult<StochRSIConfig>> results = new List<BacktestResult<StochRSIConfig>>();

        await Task.WhenAll(
            Enumerable.Range(1, 1).Select(async buySignal =>
            {
                await Task.WhenAll(
                    Enumerable.Range(100, 1).Select(async sellSignal =>
                    {
                        BackTest<StochRSIConfig> stochRSIResult = new BackTest<StochRSIConfig>(
                            ref klinesBatchedData,
                            new StochRSIConfig
                            {
                                Symbol = config.Symbol,
                                ShowConsole = config.ShowConsole,
                                Balance = config.Balance,
                                TransactionCosts = config.TransactionCosts,
                                BackTestType = config.BackTestType,
                                Interval = interval,
                                Period = period,
                                Smooth = smooth,
                                StopLoss = kstepLoss,
                                TakeProfit = jstepProfit,
                                BuySignalStochRSI = buySignal,
                                SellSignalStochRSI = sellSignal,
                            });

                        BacktestResult<StochRSIConfig> stochRSI = stochRSIResult.GetStochRSI();
                        lock (results)
                        {
                            results.Add(stochRSI);
                        }
                        //lock (results)
                        //{
                        //    if (results.Count() >= 3)//Optymalizacja pamiêci dla s³abych wyników
                        //    {
                        //        if (results.Last().TotalProfitLossLast < stochRSI.TotalProfitLossLast)
                        //        {
                        //            results.RemoveAt(results.Count() - 1);
                        //            results.Add(stochRSI);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        results.Add(stochRSI);
                        //    }
                        //}
                    })
                );
            })
        );

        return results;
    }




    #region oldVersion
    static async Task<List<BacktestResult<StochRSIConfig>>?> RunBackTestSync(IBinanceService binanceService, Config config)
    {
        if (config.BackTestType == BackTestType.StochRSI)
        {
            
            KlineInterval[] intervals = (KlineInterval[])Enum.GetValues(typeof(KlineInterval));
            int startInterval = 2; // FiveMinutes
            int endInterval = 8;

            List<BacktestResult<StochRSIConfig>> ListBackTestResult = new List<BacktestResult<StochRSIConfig>>();

            for (int i = startInterval; i <= endInterval; i++)
            {
                List<IBinanceKline> klinesBatchedData = await binanceService.GetKlinesBatchedAsync(
                    config.Symbol,
                    intervals[i],
                    config.StartDate,
                    config.EndDate);

                ListBackTestResult.AddRange(RunBacktestForParameters(klinesBatchedData, intervals[i], config));
            }
            return ListBackTestResult;
        }

        return null;
    }

    static List<BacktestResult<StochRSIConfig>> RunBacktestForParameters(List<IBinanceKline> klinesBatchedData, KlineInterval interval, Config config)
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
                        results.AddRange(RunBacktestForConfig(klinesBatchedData, interval, period, smooth, jstepProfit, kstepLoss, config));
                    }
                }
            }
        }

        return results;
    }

    static List<BacktestResult<StochRSIConfig>> RunBacktestForConfig(List<IBinanceKline> klinesBatchedData, KlineInterval interval, int period, int smooth, decimal jstepProfit, decimal kstepLoss, Config config)
    {
        List<BacktestResult<StochRSIConfig>> results = new List<BacktestResult<StochRSIConfig>>();

        for (int buySignal = 1; buySignal <= 1; buySignal += 1)
        {
            for (int sellSignal = 100; sellSignal <= 100; sellSignal += 5)
            {
                BackTest<StochRSIConfig> stochRSIResult = new BackTest<StochRSIConfig>(
                    ref klinesBatchedData,
                    new StochRSIConfig
                    {
                        Symbol = config.Symbol,
                        ShowConsole = config.ShowConsole,
                        Balance = config.Balance,
                        TransactionCosts = config.TransactionCosts,
                        BackTestType = config.BackTestType,
                        Interval = interval,
                        Period = period,
                        Smooth = smooth,
                        StopLoss = kstepLoss,
                        TakeProfit = jstepProfit,
                        BuySignalStochRSI = buySignal,
                        SellSignalStochRSI = sellSignal,
                    });

                BacktestResult<StochRSIConfig> stochRSI = stochRSIResult.GetStochRSI();
                lock (results)
                {
                    if (results.Count() >= 3)//Optymalizacja pamieci dla slabych wynikow
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
    #endregion

}








//TOP 10
