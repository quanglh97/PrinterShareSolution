using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintShareSolution.Data.Entities;
using PrintShareSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Configurations
{
    public class OrderSendFileConfiguration : IEntityTypeConfiguration<OrderSendFile>
    {
        public void Configure(EntityTypeBuilder<OrderSendFile> builder)
        {
            builder.ToTable("OrderSendFiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ReceiveId).HasMaxLength(8).IsRequired(true);
            builder.Property(x => x.FileName).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.FilePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.FileSize);
            builder.Property(x => x.DateTime);
            builder.HasOne(x => x.AppUser).WithMany(x => x.OrderSendFiles).HasForeignKey(x => x.UserId);
        }
    }
}
