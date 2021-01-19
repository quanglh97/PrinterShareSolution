using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintShareSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Configurations
{
    class OrderPrintFileConfiguration : IEntityTypeConfiguration<OrderPrintFile>
    {
        public void Configure(EntityTypeBuilder<OrderPrintFile> builder)
        {
            builder.ToTable("OrderPrintFiles");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.DateTime);
            builder.Property(x=>x.FileName).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.FilePath).HasMaxLength(200).IsRequired(true);

            builder.HasOne(x => x.Printer).WithMany(x => x.OrderPrintFiles).HasForeignKey(x => x.PrinterId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.OrderPrintFiles).HasForeignKey(x => x.UserId);
        }
    }
}
