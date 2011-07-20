using System.Data;
using FizzWare.NBuilder;
using NUnit.Framework;
using SchemaBuilder;
using Tests.TestDatabase;
using System.Linq;
using Should;

namespace AutoMigrator.Tests.NhiberanteSchemaBuilder
{
    [TestFixture]
    public class ColumnSchemaGeneratorSpecs : NhibernateBaseTester
    {
        ISchemaBuilder schema_builder;
        DbSchema schema;
        TableSchema table;

        [SetUp]
        public void SetUp() {

            
            
            schema_builder = new NhibernateSchemaBuilder(config);
            table = schema_builder.GetSchema().Tables.FirstOrDefault(x => x.TableName == "Employees");
        }

        [Test]
        public void should_create_the_columns()
        {
            var columns = table.Columns;
            columns.Count().ShouldEqual(14);
        }

        [Test]
        public void should_set_the_properties_on_the_column_schema()
        {
            var ship_name_column = table.Columns.Single(c => c.ColumnName == "Last Name");
            ship_name_column.DatabaseType.ShouldEqual(DbType.String);
            ship_name_column.Size.ShouldEqual(20);
            ship_name_column.IsPrimaryKey.ShouldBeFalse();
        }

        [Test]
        public void should_set_the_primary_key_field()
        {
            var pk = table.Columns.Single(c => c.ColumnName == "EmployeeID");
            pk.DatabaseType.ShouldEqual(DbType.Int32);
            pk.IsPrimaryKey.ShouldBeTrue();

        }
    }
}