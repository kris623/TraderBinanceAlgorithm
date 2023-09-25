namespace CalculateModule.BackTestRSI.Signals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static CalculateModule.BackTestRSI.TypesContainer;

    public static class StochRSISignals
    {
        public static (SignalType, TypesContainer.PriceType) StochRSIShouldBuy(double[] stochRsiClose, 
            int currentIndex, 
            double[] stochRsiCloseSignal,
            int variableBuy)
        {
            double k = stochRsiClose[currentIndex];
            double d = stochRsiCloseSignal[currentIndex];

            if (k > d && d < variableBuy)
            {
                return (SignalType.Buy, TypesContainer.PriceType.Close);
            }
            else
            {
                return (SignalType.None, TypesContainer.PriceType.Close);
            }
        }

        public static (SignalType, TypesContainer.PriceType) StochRSIShouldSell(double[] stochRsiClose, int currentIndex, double[]stochRsiCloseSignal, int variableSell)
        {
            double k = stochRsiClose[currentIndex];
            double d = stochRsiCloseSignal[currentIndex];

            if ((k < d && d > variableSell))
            {
                return (SignalType.Sell, TypesContainer.PriceType.Close);
            }
            else
            {
                return (SignalType.None, TypesContainer.PriceType.Close);
            }
        }
    }
}
