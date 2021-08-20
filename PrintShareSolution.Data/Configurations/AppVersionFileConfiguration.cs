using Microsoft.EntityFrameworkCore;
using PrintShareSolution.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Configurations
{
    public class AppVersionFileConfiguration : IEntityTypeConfiguration<AppVersionFile>
    {
        public void Configure(EntityTypeBuilder<AppVersionFile> builder)
        {
            builder.ToTable("AppVersionFile");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Id).IsRequired(true);
            builder.Property(x => x.Version).IsRequired(true);
            builder.Property(x => x.FileName).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.FilePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.FilePathSetup).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.FileSize);
            builder.Property(x => x.Md5).IsRequired(true);
            builder.Property(x => x.DateTime);
        }
    }
}
