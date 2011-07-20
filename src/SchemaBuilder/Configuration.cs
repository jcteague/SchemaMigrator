using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;

namespace SchemaBuilder
{
    public static class SchemaEngines
    {
        public static readonly string NHibernate = "Nhibernate";
    }
    public static class MigrationEngines
    {
        public static readonly string FluenMigrator = "FluenMigrator";
    }
    
    public class MigrationConfiguration :IConfiguration
    {
        public MigrationConfiguration(
            string connectionString,
            Func<IConfiguration, ISchemaBuilder> configureSchemaBuilder,
            Func<IConfiguration, IMigrationGenerator> configureGenerator,
            Func<IConfiguration,IMigratorExecutor> configurExecutor,
            Func<dynamic> migrationEngineOptions,
            Func<dynamic> schemaEngineOptions
     
            )
        {
            
            ConnectionString = connectionString;
            MigrationEngineOptions = migrationEngineOptions();
            SchemaEngineOptions = schemaEngineOptions();
            MigrationGenerator = configureGenerator(this);
            SchemaBuilder = configureSchemaBuilder(this);
            MigrationExector = configurExecutor(this);
     
        }

        

        public ISchemaBuilder SchemaBuilder { get; private set; }
        public IMigrationGenerator MigrationGenerator { get; set; }
        public string DatabaseType { get; private set; }
        public string ConnectionString { get; private set; }
        public IMigratorExecutor MigrationExector { get; private set; }
        public int Timeout { get; private set; }
        public dynamic MigrationEngineOptions { get; private set; }
        public dynamic SchemaEngineOptions { get; private set; }
    }
}