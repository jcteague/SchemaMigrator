using System;
using AutoMigrator.Tests;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace Tests.TestDatabase
{
    public static class TestDatabaseSettings
    {

        public const string MigrationTestDbConnectionString = "Data Source=localhost;Initial Catalog=AutoMigrationTestDb;Integrated Security=SSPI;";
        public const string ConnectionString = "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=SSPI;";
    }
    public class NHiberanteConfiguration
    {
        public static Configuration BuildConfiguration(string connectionString = TestDatabaseSettings.ConnectionString,Action<MappingConfiguration> configuration = null)
        {
            if (configuration == null) configuration = m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>();
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                .Mappings(configuration)
                .BuildConfiguration()
                .SetProperty("default_schema", "dbo");
        }
    }
}