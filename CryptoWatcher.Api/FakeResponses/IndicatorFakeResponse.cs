﻿using System.Collections.Generic;
using CryptoWatcher.Api.Responses;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class IndicatorFakeResponse
    {
        public static IndicatorResponse GetFake_PriceChangeIndicator()
        {
            return new IndicatorResponse
            {
                UserId = "cesarbmx",
                Name = "Price Change 24 Hrs",
                Description = "It identifies inmediate price changes within 24 Hrs in your portfolio",
                Formula = "C# formula"
            };
        }
        public static IndicatorResponse GetFake_HypeIndicator()
        {
            return new IndicatorResponse
            {
                UserId = "cesarbmx",
                Name = "Hype",
                Description = "It identifies inmediate hypes within 24 Hrs in your portfolio",
                Formula = "C# formula"
            };
        }
        public static List<IndicatorResponse> GetFake_List()
        {
            return new List<IndicatorResponse>
            {
                GetFake_PriceChangeIndicator(),
                GetFake_HypeIndicator()
            };
        }
    }
}
