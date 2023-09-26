namespace CalculateModule.ConfigFiles
{
    using System.Collections.Generic;
    using System.Globalization;
    using static CalculateModule.BackTestRSI.TypesContainer;

    public class Config
    {
        public required string Symbol { get; set; }
        public required bool ShowConsole { get; set; } = true;
        public required BackTestType BackTestType { get; set; }
        public required decimal Balance { get; set; } = 1000M;
        public required decimal TransactionCosts { get; set; } = 0.01M;
        public DateTime StartDate { get; set; } = DateTime.ParseExact("2023-09-01 09:15:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        public DateTime EndDate { get; set; } = DateTime.Now;

    }


    public class StochRSIConfig : Config
    {
        public Binance.Net.Enums.KlineInterval Interval { get; set; }
        public int Period { get; set; } = 14;
        public int Smooth { get; set; } = 4;
        public int SellSignalStochRSI { get; set; } = 90;
        public int BuySignalStochRSI { get; set; } = 10;
        internal decimal BalanceStart { get; set; }
        public decimal StopLoss { get; set; } = 0.10M;
        public decimal TakeProfit { get; set; } = 0.10M;
        internal int KlinesLength { get; set; }
    }


    public class RSIConfig : Config
    {
        public int Period { get; set; } = 14;
        public int Smooth { get; set; } = 4;
        public int SellStochRSI { get; set; } = 90;
        public int BuyStochRSI { get; set; } = 10;
        internal decimal BalanceStart { get; set; }
        public decimal StopLoss { get; set; } = 0.10M;
        public decimal TakeProfit { get; set; } = 0.10M;
        internal int KlinesLength { get; set; }
    }
}
