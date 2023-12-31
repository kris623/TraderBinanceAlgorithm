﻿using CalculateModule.BackTestRSI.Signals;
using CalculateModule.ConfigFiles;
using CalculateModule.ConfigFiles.Classes;
using Skender.Stock.Indicators;
using static CalculateModule.BackTestRSI.TypesContainer;

namespace BinanceModule.BackTestRSI
{

    public interface IStochRSIBackTester
    {
        bool CheckTakeProfit(decimal positionValue, decimal position, decimal closePrice);
        bool CheckTakeLoss(decimal positionValue, decimal position, decimal closePrice);
        BacktestResult<StochRSIConfig> Run(double[] closePrices, int variableSell, int variableBuy, DateTime[] closeTime);
    }

    public class StochRSIBackTester : StochRSIBackTesterVariables, IStochRSIBackTester
    {
        private readonly decimal StopLoss;
        private readonly decimal TakeProfit;
        private decimal TransactionCosts;

        public StochRSIBackTester(
            decimal stopLoss,
            decimal takeProfit,
            decimal transactionCosts,
            List<StochRsiResult> resultsClose,
            StochRSIConfig backTestConfig)
            : base(backTestConfig)
        {
            this.StopLoss = stopLoss;
            this.TakeProfit = takeProfit;
            this.TransactionCosts = transactionCosts;

            this.StochRsiCloseSignal = resultsClose.Select(x => x.Signal ?? 50).ToArray();
            this.StochRsiClose = resultsClose.Select(x => x.StochRsi ?? 50).ToArray();
            this.BackTestConfig = backTestConfig;
        }

        public bool CheckTakeProfit(decimal positionValue, decimal position, decimal closePrice)
        {
            var lastPrice = positionValue * position;
            var actualPrice = closePrice * position;
            return (actualPrice - lastPrice) >= (lastPrice * TakeProfit);
        }

        public bool CheckTakeLoss(decimal positionValue, decimal position, decimal closePrice)
        {
            var lastPrice = positionValue * position;
            var actualPrice = closePrice * position;
            return (lastPrice - actualPrice) >= (lastPrice * StopLoss);
        }

        //public TimeResultStochRSI GetTimeResultStochRSI(decimal closePrice, DateTime closeTime, decimal stochRsiClose, decimal stochRsiCloseSignal)
        //{ 
        //    return new TimeResultStochRSI { BuyCoinPrice = closePrice, StartDateClose = closeTime, EndCloseStochRSI_K = stochRsiClose, StartCloseStochRSI_D = stochRsiCloseSignal } ;
        //}

        public BacktestResult<StochRSIConfig> Run(double[] closePrices, int stochRsiSell, int strochRsiBuy, DateTime[] closeTime)
        {
            for (int i = 100; i < closePrices.Length; i++)
            {

                (SignalType, PriceType) signal = 
                    Position != 0.0M ?
                    StochRSISignals.StochRSIShouldSell(StochRsiClose, i, StochRsiCloseSignal, stochRsiSell) :
                    StochRSISignals.StochRSIShouldBuy(StochRsiClose, i, StochRsiCloseSignal, strochRsiBuy);

                decimal _tmpClosePrice = (decimal)closePrices[i];
                bool _tmpTakeProfit = CheckTakeProfit(PositionValue, Position, _tmpClosePrice);
                bool _tmpStopLoss = CheckTakeLoss(PositionValue, Position, _tmpClosePrice);


                if (Position != 0.0M && (_tmpTakeProfit || _tmpStopLoss))
                {
                    decimal profitLoss = (_tmpClosePrice - PositionValue) / PositionValue;
                    TotalProfitLoss += profitLoss;
                    BackTestConfig.Balance += (decimal)(_tmpClosePrice * Position - TransactionCosts);
                    Position = 0.0M;
                    PositionValue = 0.0M;
                    NumberOfTrades++;

                    if (profitLoss > 0)
                        NumberOfWinningTrades++;
                    else
                        NumberOfLosingTrades++;

                    //TimeResultStochRSI.Add(GetTimeResultStochRSI(_tmpClosePrice, closeTime[i], (decimal)StochRsiClose[i], (decimal)StochRsiCloseSignal[i]));

                    #region console#0
                    ////Console
                    if (BackTestConfig.ShowConsole)
                    {
                        Console.WriteLine($"({closeTime[i]}) Price:{closePrices[i]} PriceSell:{_tmpClosePrice}\nSELLNOW:{i} {StochRsiClose[i]:N2} {StochRsiCloseSignal[i]:N2}\nBalance:{BackTestConfig.Balance:N2}"); ;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    #endregion

                }
                else if (signal.Item1 == SignalType.Buy && Position == 0.0M)
                {
                    decimal priceBuy = 0;
                    if (signal.Item2 == PriceType.Close)
                    {
                        priceBuy = (decimal)_tmpClosePrice;//closePrices[i];
                    }

                    //Update data
                    Position = BackTestConfig.Balance / priceBuy;
                    PositionValue = priceBuy;
                    BackTestConfig.Balance -= (decimal)(PositionValue * Position);
                    TransactionCosts = Position * priceBuy * BackTestConfig.TransactionCosts;

                    //TimeResultStochRSI.Add(GetTimeResultStochRSI(_tmpClosePrice, closeTime[i], (decimal)StochRsiClose[i], (decimal)StochRsiCloseSignal[i]));

                    #region console#1
                    if (BackTestConfig.ShowConsole)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"({closeTime[i]}) Price:{closePrices[i]}\nBUY i :{i} {StochRsiClose[i]:N2} {StochRsiCloseSignal[i]:N2} \nBalance:{BackTestConfig.Balance:N2}"); ;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    #endregion
                }
                else if (signal.Item1 == SignalType.Sell && Position != 0.0M)
                {
                    decimal priceSell = 0;
                    if (signal.Item2 == PriceType.Close)
                        priceSell = _tmpClosePrice;//closePrices[i];

                    decimal profitLoss = (priceSell - PositionValue) / PositionValue;

                    if (profitLoss > 0)
                        NumberOfWinningTrades++;
                    else
                        NumberOfLosingTrades++;

                    TotalProfitLoss += profitLoss;
                    BackTestConfig.Balance += (decimal)(priceSell * Position - TransactionCosts);
                    Position = 0.0M;
                    PositionValue = 0.0M;
                    NumberOfTrades++;

                    //TimeResultStochRSI.Add(GetTimeResultStochRSI(_tmpClosePrice, closeTime[i], (decimal)StochRsiClose[i], (decimal)StochRsiCloseSignal[i]));


                    #region console#2
                    if (BackTestConfig.ShowConsole)
                    {
                        //profitLoss > 0 ? Console.ForegroundColor = ConsoleColor.DarkGreen : Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"({closeTime[i]}) Price:{closePrices[i]}\nSELL i:{i} {StochRsiClose[i]:N2} {StochRsiCloseSignal[i]:N2}\nBalance:{BackTestConfig.Balance:N2}"); ;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    #endregion
                }

                #region console#3
                //EveryCycle
                else if (true)
                {
                    decimal current = (BackTestConfig.Balance != 0) ? BackTestConfig.Balance : _tmpClosePrice * Position - TransactionCosts;
                    if (BackTestConfig.ShowConsole)
                    {
                        Console.WriteLine($"({closeTime[i]}) Price:{closePrices[i]} i:{i} {StochRsiClose[i]:N2} {StochRsiCloseSignal[i]:N2} Balance:{current:N2}"); ;
                    }
                }
                #endregion

                #region DetailedObject
                EquityCurve_Class Ec_Class = new();
                EquityCurve[i] = BackTestConfig.Balance + PositionValue * Position;
                Ec_Class.EquityCurve = EquityCurve[i];
                Ec_Class.CurrentPrice = _tmpClosePrice;
                Ec_Class.Hold = Position != 0.0M ? 1 : 0;
                Ec_Class.CurrentCapital = BackTestConfig.Balance + _tmpClosePrice * Position;
                EqClass[i] = Ec_Class;
                CurrentCapital[i] = BackTestConfig.Balance + _tmpClosePrice * Position;
                #endregion
            }

            double winRate = (double)NumberOfWinningTrades / NumberOfTrades * 100;

            return new BacktestResult<StochRSIConfig>()
            {
                //TimeResultStochRSIAll = TimeResultStochRSI,
                //EquityCurve = equityCurve,
                //CurrentCapital = currentCapital,
                //EquityCurve_Class = EqClass,
                TotalProfitLossLast = EquityCurve.Last(),
                TotalProfitLoss = BackTestConfig.Balance == 0 ? 
                EquityCurve.Last() - BackTestConfig.BalanceStart : BackTestConfig.Balance - BackTestConfig.BalanceStart,
                NumberOfTrades = NumberOfTrades,
                NumberOfWinningTrades = NumberOfWinningTrades,
                NumberOfLosingTrades = NumberOfLosingTrades,
                WinRate = winRate
            };
        }
    }
}

