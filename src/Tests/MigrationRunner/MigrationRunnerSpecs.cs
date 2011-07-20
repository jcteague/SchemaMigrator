using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using ClaySharp;
using FluentMigrator.Expressions;
using FluentMigrator.Model;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SchemaBuilder;
using SchemaMigration.FluentMigrator;
using Tests.TestDatabase;

namespace AutoMigrator.Tests.MigrationRunner
{
    [TestFixture]
    public class FluentMigrationExecutorSpecs
    {
        FluentMigratorExecutor runner;
        IConfiguration configuration;
        IEnumerable<dynamic> migrations;
        dynamic New = new ClayFactory();

        [TestFixtureSetUp]
        public void tfsa()
        {
            WipeDatabase();
        }

        [SetUp]
        public void SetUp()
        {
            configuration = create_configuration();
            runner = new FluentMigratorExecutor(configuration);
            
            migrations = CreateMigration();
        }

        [Test]
        public void should_create_the_table()
        {
            runner.ExecuteMigrations(migrations);
            Console.WriteLine(runner.GetOutput());
            
        }
        
        IConfiguration create_configuration()
        {
            var nh_config = NHiberanteConfiguration.BuildConfiguration();
            dynamic migration_config = new ExpandoObject();
            migration_config.DatabaseType = "SqlServer";
            migration_config.Timeout = 30;
            dynamic schema_generator_config = new ExpandoObject();
            schema_generator_config.Configuration = nh_config;

            return new MigrationConfiguration(TestDatabaseSettings.MigrationTestDbConnectionString,
                (cfg) => new NhibernateSchemaBuilder(cfg),
                (cfg) => new FluentMigratorGenerator(),
                (cfg) => new FluentMigratorExecutor(cfg),
                ()=>  New.MigrationConfig(DatabaseType: "SqlServer",Timeout:30),
                ()=> New.SchemaGeneratorConfig(Configuration: nh_config)
                );
        }

        void WipeDatabase()
        {
            var cfg = NHiberanteConfiguration.BuildConfiguration(connectionString:TestDatabaseSettings.MigrationTestDbConnectionString);
            var exp = new SchemaExport(cfg);
            exp.Execute(true,true,true);
            
        }

        static IEnumerable<dynamic> CreateMigration()
        {
            var table_migration =  new CreateTableExpression();
            table_migration.TableName = "MigrationRunnerTest";
            table_migration.Columns.Add(new ColumnDefinition{Name="Runner",Type = DbType.String,Size = 20});
            return new[] {table_migration};
        }
    }
}