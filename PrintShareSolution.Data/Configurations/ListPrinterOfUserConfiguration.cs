using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintShareSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Configurations
{
    public class ListPrinterOfUserConfiguration : IEntityTypeConfiguration<ListPrinterOfUser>
    {
        public void Configure(EntityTypeBuilder<ListPrinterOfUser> builder)
        {
            builder.ToTable("ListPrinterOfUser");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Printer).WithMany(x => x.ListPrinterOfUsers).HasForeignKey(x => x.PrinterId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.ListPrinterOfUsers).HasForeignKey(x => x.UserId);
        }
    }
}
