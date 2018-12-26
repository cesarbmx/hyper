﻿using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CryptoWatcher.Application.Requests
{
    public class UpdateWatcherRequest
    {
        [JsonIgnore] public Guid WatcherId { get; set; }
        [Required] public decimal Buy { get; set; }
        [Required] public decimal Sell { get; set; }
        [Required] public bool Enabled { get; set; }
    }
}
