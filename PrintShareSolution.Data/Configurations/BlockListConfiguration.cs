using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintShareSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Configurations
{
    public class BlockListConfiguration : IEntityTypeConfiguration<BlockList>
    {
        public void Configure(EntityTypeBuilder<BlockList> builder)
        {
            //Id, Name, UserId, Status
            builder.ToTable("BlockLists");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.UserBlocked);
            builder.Property(x => x.BlackListFilePath).HasMaxLength(200);
            builder.HasOne(x => x.AppUser).WithMany(x => x.BlockIds).HasForeignKey(x => x.UserId);
        }
    }
}
