using System;
using System.Collections.Generic;
using System.Data;

namespace SchemaBuilder
{
    public interface ISchemaBuilder
    {
        DbSchema GetSchema();
    }

    public class DbSchema
    {
        private List<TableSchema> tables = new List<TableSchema>();
        public IEnumerable<TableSchema> Tables { get { return tables; } }

        public void AddTable(TableSchema tableSchema)
        {
            tables.Add(tableSchema);
        }
    }
    public class TableSchema
    {
        List<ColumnSchema> _columns = new List<ColumnSchema>();
        public string TableName { get; set; }
        public IEnumerable<ColumnSchema> Columns { get { return _columns; } }

        public string SchemaName { get; set; }

        public void AddColumn(ColumnSchema column)
        {
            _columns.Add(column);
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            TableSchema ts_obj = obj as TableSchema;
            if (ts_obj == null) return false;
            return ts_obj.TableName.Equals(TableName);
        }
        public override int GetHashCode() {
            return TableName.GetHashCode();
        }
    }
    public class ColumnSchema
    {
        readonly string _columnName;

        public ColumnSchema()
        {
            
        }
        public string ColumnName { get; set; }
        public string TableName { get; set; }
        public object DefaultValue { get; set; }
        public bool IsForiegnKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public int Precision { get; set; }
        public string PrimaryKeyName { get; set; }
        public int Size { get; set; }
        public DbType? DatabaseType { get; set; }
        public int Scale { get; set; }
    }
}