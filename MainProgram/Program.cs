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


InitServices initialService = new InitServices();
IDbService dbService = initialService.GetServices().GetRequiredService<IDbService>();
IBinanceService binanceService = initialService.GetServices().GetRequiredService<IBinanceService>();
binanceService.AuthBinanceService("APIKEY", "APISECRET");


string symbol = "LINAUSDT";
KlineInterval interval = KlineInterval.ThirtyMinutes;
DateTime startDate = DateTime.ParseExact("2023-09-01 09:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
DateTime endDate = DateTime.Now;


List<IBinanceKline> klinesBatchedData = await binanceService.GetKlinesBatchedAsync(
    symbol,
    interval,
    startDate,
    endDate);


BackTest<StochRSIConfig> stochRSIResult = new BackTest<StochRSIConfig>(
    ref klinesBatchedData, 
    new StochRSIConfig
    {
        ShowConsole = true,
        BackTestType = BackTestType.StochRSI,
        Interval = interval,
        Period = 14,
        Smooth = 4,
        StopLoss = 0.10M,
        TakeProfit = 0.10M,
        TransactionCosts = 0.01M,
        Balance = 500
    });

BacktestResult stochRSI = stochRSIResult.GetStochRSI();

Console.WriteLine($"Total profit/loss: {stochRSI.TotalProfitLoss:N2} $");
Console.WriteLine($"Number of trades: " + stochRSI.NumberOfTrades);
Console.WriteLine($"Number of winning trades: " + stochRSI.NumberOfWinningTrades);
Console.WriteLine($"Number of losing trades: " + stochRSI.NumberOfLosingTrades);
Console.WriteLine($"Win rate: {stochRSI.WinRate:N2} + %");

//await dbService.SaveStrategy(PrepareData.ObjectPrepareStochRSI(ref stochRSIResult, ref stochRSI));

Console.ReadLine();



