using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintShareSolution.Data.Entities;
using PrintShareSolution.Data.Enums;

namespace PrintShareSolution.Data.Configurations
{
    public class PrinterConfiguration : IEntityTypeConfiguration<Printer>
    {
        public void Configure(EntityTypeBuilder<Printer> builder)
        {
            //Id, Name, UserId, Status
            builder.ToTable("Printers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(Status.InActive);
        }

    }
}
