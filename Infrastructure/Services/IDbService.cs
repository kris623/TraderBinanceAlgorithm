using DTO.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public interface IDbService
    {
        Task SaveStrategy(StrategyMemory rsiStrategyResult);
        Task SaveListStrategy(List<StrategyMemory> listRSIStrategyResult);
    }
}