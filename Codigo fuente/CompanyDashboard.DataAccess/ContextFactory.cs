using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CompanyDashboard.DataAccess
{

    public class ContextFactory : IDesignTimeDbContextFactory<CompanyDashboardContext>
    {

        public enum ContextType
        {
            MEMORY, SQL
        }
        public CompanyDashboardContext CreateDbContext(string[] args)
        {
            return GetNewContext();

        }

        public static CompanyDashboardContext GetNewContext(ContextType type = ContextType.SQL)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<CompanyDashboardContext>();
            builder.UseSqlServer(connectionString);

            return new CompanyDashboardContext(builder.Options);
        } 
    }
}