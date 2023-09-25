namespace CalculateModule.ConfigFiles.Classes
{
    using BinanceModule.BackTestRSI;
    using CalculateModule.ConfigFiles;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StochRSIBackTesterVariables
    {
        public decimal position { get; set; }
        public decimal positionValue { get; set; }
        public decimal totalProfitLoss { get; set; }
        public int numberOfTrades { get; set; }
        public int numberOfWinningTrades { get; set; }
        public int numberOfLosingTrades { get; set; }
        public double[] StochRsiClose { get; set; }
        public double[] StochRsiCloseSignal { get; set; }
        public decimal[] EquityCurve { get; set; }
        public decimal[] currentCapital { get; set; }
        public EquityCurve_Class[] EqClass { get; set; }
        public List<TimeResultStochRSI> TimeResultStochRSI { get; set; }
        public StochRSIConfig BackTestConfig { get; set; }
        public StochRSIBackTesterVariables(StochRSIConfig backTestConfig)
        {
            BackTestConfig = backTestConfig;
            EquityCurve = new decimal[backTestConfig.KlinesLength];
            currentCapital = new decimal[backTestConfig.KlinesLength];
            EqClass = new EquityCurve_Class[backTestConfig.KlinesLength];
            TimeResultStochRSI = new List<TimeResultStochRSI>();
            StochRsiClose = new double[backTestConfig.KlinesLength];
            StochRsiCloseSignal = new double[backTestConfig.KlinesLength];
        }
    }


    public class BacktestResult
    {
        public EquityCurve_Class[]? EquityCurve_Class { get; set; }
        public double[]? EquityCurve { get; set; }
        public List<TimeResultStochRSI>? TimeResultStochRSIAll { get; set; }
        public decimal[]? CurrentCapital { get; set; }
        public decimal TotalProfitLoss { get; set; }
        public decimal TotalProfitLossLast { get; set; }
        public int NumberOfTrades { get; set; }
        public int NumberOfWinningTrades { get; set; }
        public int NumberOfLosingTrades { get; set; }
        public double WinRate { get; set; }
        public int StochRSIValue { get; set; }
        public int StochRSIPeriodValue { get; set; }
        public double stopLossValue { get; set; }
        public double takeProfitValue { get; set; }
        public string? intervalString { get; set; }
        public int buyStochRSI { get; set; }
        public int sellStochRSI { get; set; }
    }

    public class TimeResultStochRSI
    {
        public int WinLoss { get; set; }
        public decimal SellCoinPrice { get; set; }

        public decimal SellPrice { get; set; }

        public decimal BuyPrice { get; set; }
        public decimal BuyCoinPrice { get; set; }

        public decimal CurrentCapital { get; set; }

        public DateTime? StartDateClose { get; set; }
        public DateTime? EndDateClose { get; set; }

        public decimal StartCloseStochRSI_K { get; set; }
        public decimal StartCloseStochRSI_D { get; set; }

        public decimal EndCloseStochRSI_K { get; set; }
        public decimal EndCloseStochRSI_D { get; set; }
    }

    public class EquityCurve_Class //zakomentowane
    {
        public decimal EquityCurve { get; set; }

        public decimal CurrentPrice { get; set; }

        public int Hold { get; set; }

        public decimal CurrentCapital { get; set; }
    }

}
