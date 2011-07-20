using System;
using System.Collections.Generic;

namespace SchemaBuilder
{
    

    public interface IMigrationRunner
    {
        void RunMigrations();
    }
    
    public class MigrationRunner : IMigrationRunner
    {
        readonly IMigrationRepository repository;
        readonly IConfiguration configuration;
        readonly ISchemaBuilder schema_builder;
        readonly MigrationBuilder migration_builder;
        IMigrationGenerator migration_generator;
        IMigratorExecutor migration_executor;


        public MigrationRunner(IMigrationRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
            schema_builder = configuration.SchemaBuilder;
            migration_builder = new MigrationBuilder();
            migration_generator = configuration.MigrationGenerator;
            migration_executor = configuration.MigrationExector;


        }

        public void RunMigrations()
        {
            var current_schema = repository.GetCurrentSchema();
            var new_schema = schema_builder.GetSchema();
            var migrations = migration_builder.GenerateSchemaDiff(current_schema, new_schema);
            IEnumerable<dynamic> generated_migrations = migration_generator.GenerateMigrations(migrations);
            migration_executor.ExecuteMigrations(generated_migrations);
            repository.UpdateCurrentMigration(migrations);

        }
    }

    

    public interface IMigrationRepository
    {
        DbSchema GetCurrentSchema();
        void UpdateCurrentMigration(Migration[] migration_changes);
    }
    public interface IConfiguration
    {
        ISchemaBuilder SchemaBuilder { get; }
        IMigrationGenerator MigrationGenerator { get; set; }
        string DatabaseType { get; }
        string ConnectionString { get; }
        IMigratorExecutor MigrationExector { get; }
        int Timeout { get; }
        dynamic MigrationEngineOptions { get; }
        dynamic SchemaEngineOptions { get; }
        
    }
}