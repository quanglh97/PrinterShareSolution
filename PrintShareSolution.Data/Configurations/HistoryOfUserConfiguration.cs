using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintShareSolution.Data.Entities;
using PrintShareSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Configurations
{
    public class HistoryOfUserConfiguration : IEntityTypeConfiguration<HistoryOfUser>
    {
        public void Configure(EntityTypeBuilder<HistoryOfUser> builder)
        {
            builder.ToTable("HistoryOfUsers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ReceiveId);
            builder.Property(x => x.PrinterId);
            builder.Property(x => x.FileName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.FileSize);
            builder.Property(x => x.ActionHistory);
            builder.Property(x => x.DateTime);
            builder.Property(x => x.Pages);
            builder.Property(x => x.Result).HasDefaultValue(Result.NotDone);
            builder.Property(x => x.OrderPrintFileId);
            builder.Property(x => x.OrderSendFileId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.HistoryOfUsers).HasForeignKey(x => x.UserId);
            
        }
    }
}
