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
        internal decimal Position { get; set; }
        internal decimal PositionValue { get; set; }
        internal decimal TotalProfitLoss { get; set; }
        internal int NumberOfTrades { get; set; }
        internal int NumberOfWinningTrades { get; set; }
        internal int NumberOfLosingTrades { get; set; }
        internal double[] StochRsiClose { get; set; }
        internal double[] StochRsiCloseSignal { get; set; }
        internal decimal[] EquityCurve { get; set; }
        internal decimal[] currentCapital { get; set; }
        internal EquityCurve_Class[] EqClass { get; set; }
        internal List<TimeResultStochRSI> TimeResultStochRSI { get; set; }
        internal StochRSIConfig BackTestConfig { get; set; }
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


    public class BacktestResult<T> where T : class
    {
        public required T Configuration { get; set; }
        public decimal TotalProfitLoss { get; set; }
        public decimal TotalProfitLossLast { get; set; }
        public int NumberOfTrades { get; set; }
        public int NumberOfWinningTrades { get; set; }
        public int NumberOfLosingTrades { get; set; }
        public double WinRate { get; set; }

        //public EquityCurve_Class[]? EquityCurve_Class { get; set; }
        //public double[]? EquityCurve { get; set; }
        //public List<TimeResultStochRSI>? TimeResultStochRSIAll { get; set; }
        // public decimal[]? CurrentCapital { get; set; }
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
