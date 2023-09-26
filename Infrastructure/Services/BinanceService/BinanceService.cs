using Microsoft.Extensions.Logging;
using Binance.Net.Enums;
using Binance.Net.Clients;
using CryptoExchange.Net.Authentication;
using Binance.Net.Interfaces;
using System.Collections.Generic;
using System.Collections;

namespace Infrastructure.Services.BinanceService
{
    public class BinanceService : IBinanceService
    {
        private readonly ILogger<IBinanceService> logger;
        private readonly BinanceRestClient binanceClient;

        public BinanceService(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<BinanceService>();
            this.binanceClient = new BinanceRestClient();
        }

        public void AuthBinanceService(string ApiKeyIKEY = "a", string ApiKeySecret = "b")
        {
            BinanceRestClient.SetDefaultOptions(options =>
            {
                options.ApiCredentials = new ApiCredentials(ApiKeyIKEY, ApiKeySecret);
            });
        }

        public async Task<List<IBinanceKline>> GetKlinesAsync(string symbol, KlineInterval interval, int limit = 1000)
        {
            logger.LogInformation($"async Task<List<Binance.Net.Interfaces.IBinanceKline>> GetKlines(string {symbol}, KlineInterval {interval}, int {limit})");
            var klines = await binanceClient.SpotApi.ExchangeData.GetKlinesAsync(symbol, interval, null, null, limit);
            return klines.Data.ToList();
        }

        public async Task<List<IBinanceKline>> GetKlinesBatchedAsync(string symbol, KlineInterval interval, DateTime start, DateTime end)
        {
            logger.LogInformation($"List<IBinanceKline> GetKlinesBatched(string {symbol}, KlineInterval {interval}, DateTime {start}, DateTime {end})");

            List<IBinanceKline> klines = new List<IBinanceKline>();
            DateTime startTime = (start);
            DateTime endTime = (end);

            while (startTime < endTime)
            {
                var result = await binanceClient.SpotApi.ExchangeData.GetKlinesAsync(symbol, interval, Convert.ToDateTime(startTime), Convert.ToDateTime(endTime), null);

                if (result.Success)
                {
                    klines.AddRange(result.Data);
                }
                else
                {
                    throw new Exception("Błąd podczas pobierania świec.");
                }

                if (result.Data.Count() == 0)
                {
                    break;
                }

                startTime = result.Data.ElementAt(result.Data.Count() - 1).CloseTime;
            }

            return klines;
        }
    }

}
