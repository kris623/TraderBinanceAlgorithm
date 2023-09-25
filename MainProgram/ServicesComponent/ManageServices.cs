using Microsoft.Extensions.DependencyInjection;
using BinanceModule.BackTestRSI;
using Infrastructure.Services.BinanceService;
using Infrastructure.Services;
using Binance.Net.Enums;

namespace MainProgram.ServicesComponent
{
    //internal class ManageServices
    //{
    //    private IDbService dbService { get; set; }
    //    private IBinanceService? binanceService { get; set; }

    //    internal ManageServices(string ApiKeyIKEY, string ApiKeySecret)
    //    {
    //         InitServices initialService = new InitServices();
    //         this.dbService = initialService.GetServices().GetRequiredService<IDbService>();
    //         this.binanceService = initialService.GetServices().GetRequiredService<IBinanceService>();
    //         this.binanceService?.AuthBinanceService(ApiKeyIKEY, ApiKeySecret);
    //    }

    //    public List<Binance.Net.Interfaces.IBinanceKline> GetKlinesBatched(string symbol, KlineInterval interval, DateTime start, DateTime end)
    //    {
    //        return this.binanceService?.GetKlinesBatched(symbol, interval, start, end);
    //    }
    //}
}
