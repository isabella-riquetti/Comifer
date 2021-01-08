using Comifer.Data.Mapping;
using Comifer.Data.Models;
using Comifer.Data.Models.Mapping;
using Comifer.Data.Programmability.Functions;
using Comifer.Data.Programmability.Stored_Procedures;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;

namespace Comifer.Data.Context
{
    public class ComiferContextLog : DbContext
    {
        public static string GetConnectionString(string contextName)
        {
            var enviromentConnection = Environment.GetEnvironmentVariable(contextName);
            var defaultConnection = ConfigurationManager.ConnectionStrings[contextName]?.ConnectionString;
            var coreConnection = GetCoreConnectionString(contextName);

            var connection = defaultConnection ?? enviromentConnection ?? coreConnection ?? contextName;

            return connection;
        }

        public ComiferContextLog(string contextName) : base(GetConnectionString(contextName))
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Models.File> Image { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<ProductParent> ProductParent { get; set; }
        public DbSet<Provider> Provider { get; set; }

        public StoredProcedures StoredProcedures { get; set; }
        public ScalarValuedFunctions ScalarValuedFunctions { get; set; }
        public TableValuedFunctions TableValuedFunctions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerAddressMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new PromotionMap());
            modelBuilder.Configurations.Add(new ProductParentMap());
            modelBuilder.Configurations.Add(new ProviderMap());
        }

        private static string GetCoreConnectionString(string contextName)
        {
            var appSettingsPath = Directory.GetCurrentDirectory() + "/appsettings.json";

            if (System.IO.File.Exists(appSettingsPath))
            {
                var builder = new ConfigurationBuilder().AddJsonFile(appSettingsPath);
                var configuration = builder.Build();
                var connectionString = configuration.GetConnectionString(contextName);
                return connectionString;
            }

            return "";
        }
    }
}
