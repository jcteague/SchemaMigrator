
using System;
using System.Data;
using NHibernate.Cfg;
using NHibernate.Dialect;
using System.Linq;

namespace SchemaBuilder
{
    public class NhibernateSchemaBuilder : ISchemaBuilder
    {
        readonly Configuration _configuration;

        public NhibernateSchemaBuilder(IConfiguration configuration)
        {
            _configuration = configuration.SchemaEngineOptions.Configuration;
        }

        public DbSchema GetSchema()
        {
            var schema = new DbSchema();
            var map = _configuration.BuildMapping();
            var mappings = _configuration.ClassMappings;
            
            foreach(var class_map in mappings)
            {
                var table = class_map.Table;
                var table_schema = new TableSchema() {TableName = table.Name, SchemaName = table.Schema};
                foreach (var column in table.ColumnIterator)
                {

                    var type_code = column.GetSqlTypeCode(map);
                    
                    var columnSchema = new ColumnSchema()
                                           {
                                               TableName = table_schema.TableName,
                                               ColumnName = column.Name,
                                               IsNullable = column.IsNullable,
                                               DatabaseType = type_code.DbType,
                                               Size = column.Length,
                                               Scale = column.Scale,
                                               Precision = column.Precision,
                                               IsPrimaryKey = table.PrimaryKey.Columns.Contains(column)
                                           };
                    
                    // columnSchema.DatabaseType = property.GetSqlTypeCode(map).DbType;
                    table_schema.AddColumn(columnSchema);
                }
                schema.AddTable(table_schema);
            }
            return schema;
        }
    }
}