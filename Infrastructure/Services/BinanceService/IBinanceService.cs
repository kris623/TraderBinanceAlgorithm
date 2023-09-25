using Binance.Net.Enums;
using Binance.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.BinanceService
{
    public interface IBinanceService
    {
        Task<List<IBinanceKline>> GetKlinesAsync(string symbol, KlineInterval interval, int limit);

        Task<List<IBinanceKline>> GetKlinesBatchedAsync(string symbol, KlineInterval interval, DateTime start, DateTime end);

        void AuthBinanceService(string ApiKeyIKEY, string ApiKeySecret);

    }
}