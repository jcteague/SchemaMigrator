using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using NUnit.Framework;
using SchemaBuilder;
using SchemaMigration.FluentMigrator;
using System.Linq;
using Should;

namespace AutoMigrator.Tests.FluentMigrationMigrationGenerator {
    public class FluentMigrationSchemaGeneratorSpecs {



    }
    [TestFixture]
    public class when_generating_a_create_table_migration : FluentMigrationSchemaGeneratorSpecs {
        IMigrationGenerator generator;
        IEnumerable<Migration> migrations;
        CreateColumnMigration column_1;
        string table_name;
        dynamic result;
        string schema_name;

        [SetUp]
        public void SetUp()
        {
            table_name = "test";
            schema_name = "schema";
            column_1 = new CreateColumnMigration(new ColumnSchema(){ColumnName = "Column"});
            var createTableMigration = Builder<CreateTableMigration>
                .CreateNew()
                .With(x => x.TableName = table_name)
                .With(x=>x.SchemaName = schema_name)
                .With(x => x.Colunns = new[] {column_1}).Build();
            migrations =new[]{createTableMigration};
                
            generator = new FluentMigratorGenerator();
            result = generator.GenerateMigrations(migrations).First();

        }

        [Test]
        public void it_should_create_tyhe_table_with_table_name_and_schema_name() {
            ((string)result.TableName).ShouldEqual(table_name);
        }

        [Test]
        public void it_should_create_the_column_migrations()
        {
            ((string)result.SchemaName).ShouldEqual(schema_name);
        }

        [Test]
        public void should_add_the_the_column_definitions()
        {
            var column = result.Columns[0];
            ((string)column.Name).ShouldEqual(column_1.Column.ColumnName);
        }
        
    }

}

