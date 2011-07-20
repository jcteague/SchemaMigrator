using FizzWare.NBuilder;
using NUnit.Framework;
using SchemaBuilder;
using SchemaBuilder.Persistence;
using Should;
using System.Linq;


namespace AutoMigrator.Tests.SchemaBuilder
{
    [TestFixture]
    public class CreateTableMigrationSpecs
    {
        protected MigrationBuilder sut;
        [SetUp]
        public void SetUp()
        {
            sut = new MigrationBuilder();
        }
    }

    public class when_a_table_is_not_in_the_current_schema : CreateTableMigrationSpecs
    {
        DbSchema current_schema;
        DbSchema new_schema;
        CreateTableMigration migration;
        string table_name;
        ColumnSchema new_column;
        string column_name;

        [SetUp]
        public void SetUp()
        {
            table_name = "MyTable";
            new_column = new ColumnSchema(){ColumnName = column_name} ;
            var new_table = Builder<TableSchema>.CreateNew()
                .With(x => x.TableName = table_name)
                .Do(x => x.AddColumn(new_column)).Build();
            
                current_schema = TestSchemaData.EmptySchema;
            new_schema = new DbSchema();
            new_schema.AddTable(new_table);

            migration = (CreateTableMigration)sut.GenerateSchemaDiff(current_schema, new_schema)[0];
        }           


        [Test]
        public void it_should_create_a_create_table_migration()
        {
            
            migration.ShouldBeType<CreateTableMigration>();
            migration.TableName.ShouldEqual(table_name);
            
        }

        [Test]
        public void it_should_create_all_of_the_columns_for_the_new_table()
        {
            migration.Colunns.Where(x=>x.Column.ColumnName == column_name).ShouldNotBeEmpty();
        }
    }

    public class TestSchemaData
    {
        public static DbSchema EmptySchema = new DbSchema();
        public static TableSchema Table(string name)
        {
            return new TableSchema() { TableName = name };
        } 
    }
}