﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class CurrencyMap
    {
        public CurrencyMap(EntityTypeBuilder<Currency> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.CurrencyId);

            // Properties
            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Rank)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.Symbol)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Name)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.MarketCap)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.Volume24H)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.PercentageChange24H)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
