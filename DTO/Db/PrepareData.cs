namespace MainProgram.ServicesComponent
{
    using DTO.Db;
    using BinanceModule.BackTestRSI;
    using CalculateModule.ConfigFiles;
    using CalculateModule.ConfigFiles.Classes;
    using static CalculateModule.BackTestRSI.TypesContainer;

    public static class PrepareData
    {
        public static StrategyMemory ObjectPrepareStochRSI(ref BackTest<StochRSIConfig> backTest, ref BacktestResult<StochRSIConfig> backtestResult)
        {
            return new StrategyMemory
            {
                BackTestType = backTest.configuration.BackTestType,
                Period = backTest.configuration.Period,
                Smooth = backTest.configuration.Smooth,
                StopLoss = backTest.configuration.StopLoss,
                TakeProfit = backTest.configuration.TakeProfit,
                Balance = backTest.configuration.Balance,
                TotalProfitLoss = backtestResult.TotalProfitLoss,
                NumberOfTrades = backtestResult.NumberOfTrades,
                NumberOfWinningTrades = backtestResult.NumberOfWinningTrades,
                NumberOfLosingTrades = backtestResult.NumberOfLosingTrades,
                WinRate = backtestResult.WinRate,
                Interval = backTest.configuration.Interval,
                //stochRSIConfig = backTest.configuration; 
            };
        }
    }
}
