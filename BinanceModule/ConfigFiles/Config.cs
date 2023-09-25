namespace CalculateModule.ConfigFiles
{
    using System.Collections.Generic;
    using static CalculateModule.BackTestRSI.TypesContainer;

    public class Config
    {
        public Binance.Net.Enums.KlineInterval Interval { get; set; }
        public required bool ShowConsole { get; set; } = true;
        public required BackTestType BackTestType { get; set; }
        public required decimal Balance { get; set; } = 1000M;
        internal decimal BalanceStart { get; set; }
        public decimal StopLoss { get; set; } = 0.10M;
        public decimal TakeProfit { get; set; } = 0.10M;
        public decimal TransactionCosts { get; set; } = 0.01M;
        internal int KlinesLength { get; set; }
    }

    public class StochRSIConfig : Config
    {
        public int Period { get; set; } = 14;
        public int Smooth { get; set; } = 4;
        public int SellStochRSI { get; set; } = 90;
        public int BuyStochRSI { get; set; } = 10;
    }

    public class RSIConfig : Config
    {
        public int Period { get; set; } = 14;
        public int Smooth { get; set; } = 4;
        public int SellStochRSI { get; set; } = 90;
        public int BuyStochRSI { get; set; } = 10;
    }
}
