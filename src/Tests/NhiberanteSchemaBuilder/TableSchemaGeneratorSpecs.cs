using NUnit.Framework;
using SchemaBuilder;
using System.Linq;
using Should;
using Tests.TestDatabase;

namespace AutoMigrator.Tests.NhiberanteSchemaBuilder
{
    public class TableSchemaGeneratorSpecs
    {
     
       
   
    }

    public class when_generating_the_tables : NhibernateBaseTester
    {
        ISchemaBuilder schema_builder;
        DbSchema schema;
        const int TABLE_COUNT = 6;
        [SetUp]
        public void SetUp()
        {
            
            schema_builder = new NhibernateSchemaBuilder(config);
            schema = schema_builder.GetSchema();
        }

        [Test]
        public void should_create_table_schema_for_all_tables()
        {
            schema.Tables.Count().ShouldEqual(1);
        }

        [Test]
        public void should_set_the_table_name_and_schema()
        {
            schema.Tables.First().TableName.ShouldEqual("Customers");
            //TODO figure out how to test scheam
            //schema.Tables.First().SchemaName.ShouldEqual("dbo");
        }
    }
}