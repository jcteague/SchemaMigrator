using System;
using System.Collections.Generic;
using System.IO;
using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Processors;
using SchemaBuilder;

namespace SchemaMigration.FluentMigrator
{
    public class FluentMigratorExecutor : IMigratorExecutor
    {
        readonly IConfiguration _configuration;
        IMigrationProcessor migration_processor;
        StringWriter writer;

        public FluentMigratorExecutor(IConfiguration configuration)
        {
            _configuration = configuration;
            migration_processor = InitializeProcessor();
        }


        IMigrationProcessor InitializeProcessor()
        {
            dynamic options = _configuration.MigrationEngineOptions;
            writer = new StringWriter();
            var announcer = new TextWriterAnnouncer(writer);
            var processorFactory = ProcessorFactory.GetFactory(options.DatabaseType);
            var processor = processorFactory.Create(_configuration.ConnectionString,announcer, new ProcessorOptions
            {
                PreviewOnly = false,
                Timeout = options.Timeout
            });
            return processor;
        }

        

        public void ExecuteMigrations(IEnumerable<dynamic> generatedMigrations)
        {
            foreach (dynamic migration in generatedMigrations)
            {
                
               migration.ExecuteWith(migration_processor);
               
            }
            
        }

        public string GetOutput()
        {

            return writer.GetStringBuilder().ToString();
        }
    }
}