using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NHibernate.Hql.Ast.ANTLR.Util;
using NUnit.Framework;
using SchemaBuilder;
using SchemaMigration.FluentMigrator;
using Should;

namespace AutoMigrator.Tests.FluentMigrationMigrationGenerator
{
    public class CreateColumnSchemaGenenaratorSpecs
    {
        IMigrationGenerator generator;
        IEnumerable<Migration> migrations;
        CreateColumnMigration column_1;
        string table_name;
        dynamic result;
        string schema_name;

        [SetUp]
        public void SetUp() {
            table_name = "test";
            schema_name = "schema";
            var column_scheam = Builder<ColumnSchema>.CreateNew().Build();
            column_1 = Builder<CreateColumnMigration>.CreateNew().WithConstructor(()=> new CreateColumnMigration(column_scheam)).Build();
                
            
            migrations = new[] { column_1 };

            generator = new FluentMigratorGenerator();
            result = generator.GenerateMigrations(migrations).First();

        }

        [Test]
        public void should_setup_up_column_definition_correctly()
        {
            var column = result.Column;
            var c = column_1.Column;
            ((string)column.TableName).ShouldEqual(c.TableName);
            ((string)column.Name).ShouldEqual(c.ColumnName);
            ((string)column.PrimaryKeyName).ShouldEqual(c.PrimaryKeyName);
            ((bool)column.IsForeignKey).ShouldEqual(c.IsForiegnKey);
            ((bool)column.IsIdentity).ShouldEqual(c.IsIdentity);
            ((bool)column.IsPrimaryKey).ShouldEqual(c.IsPrimaryKey);
            ((int)column.Precision).ShouldEqual(c.Precision);
            ((string)column.DefaultValue).ShouldEqual(c.DefaultValue);
        }
    }
}