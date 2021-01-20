using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrintShareSolution.Data.EF
{
    class PrinterShareSolutionContextFactory : IDesignTimeDbContextFactory<PrinterShareDbContext>
    {
        public PrinterShareDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("AppSettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("PrinterShareSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<PrinterShareDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PrinterShareDbContext(optionsBuilder.Options);
        }
    }
}
