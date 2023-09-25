namespace DTO.Db
{
    using CalculateModule.ConfigFiles;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static CalculateModule.BackTestRSI.TypesContainer;

    public class StrategyMemory
    {
        public int ID { get; set; }
        public required BackTestType BackTestType { get;  set; }
        public int Period { get; set; }
        public int Smooth { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public required decimal Balance { get; set; }
        public decimal TotalProfitLoss { get; set; }
        public int NumberOfTrades { get; set; }
        public int NumberOfWinningTrades { get; set; }
        public int NumberOfLosingTrades { get; set; }
        public double WinRate { get; set; }
        public required Binance.Net.Enums.KlineInterval Interval {get;set; }
        public DateTime Syscreated { get; set; }

        //public required StochRSIConfig stochRSIConfig { get; set; }
    }
}
