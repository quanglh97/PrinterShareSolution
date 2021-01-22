using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintShareSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Configurations
{
    class HistoryOfUserConfiguration : IEntityTypeConfiguration<HistoryOfUser>
    {
        public void Configure(EntityTypeBuilder<HistoryOfUser> builder)
        {
            builder.ToTable("HistoryOfUsers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.FileName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ActionHistory);
            builder.Property(x => x.DateTime);

            builder.HasOne(x => x.Printer).WithMany(x => x.HistoryOfUsers).HasForeignKey(x => x.PrinterId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.HistoryOfUsers).HasForeignKey(x => x.UserId);
            
        }
    }
}
