﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Types;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class ChartService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;

        public ChartService(
            MainDbContext mainDbContext,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
        public async Task<List<Responses.Chart>> GetAllCharts(string currencyId = null, string indicatorId = null, string userId = null)
        {
            // Get all currencies
            var currencies = await _mainDbContext.Currencies.Where(CurrencyExpression.Filter(currencyId)).ToListAsync();

            // Get all indicators
            var indicators = await _mainDbContext.Indicators.Where(IndicatorExpression.Filter(indicatorId, userId)).ToListAsync();

            // Get all lines
            var lines = await _mainDbContext.Lines.Where(LineExpression.Filter(Period.ONE_MINUTE, currencyId, indicatorId, userId)).ToListAsync();

            // Build charts
            var charts = ChartBuilder.BuildCharts(currencies, indicators, lines);

            // Response
            var response = _mapper.Map<List<Responses.Chart>>(charts);

            // Return
            return response;
        }
    }
}
