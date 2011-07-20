using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SchemaBuilder
{
    public interface Migration
    {
        dynamic GetGenerator(MigrationFactory migrationFactory);
    }

    public class CreateTableMigration : Migration
    {
        public string TableName { get; set; }
        List<CreateColumnMigration> _colunns = new List<CreateColumnMigration>();
        public IEnumerable<CreateColumnMigration> Colunns
        {
            get { return _colunns; }
            set { _colunns = value.ToList(); }
        }

        public string SchemaName { get; set; }

        public dynamic GetGenerator(MigrationFactory migrationFactory)
        {
            return migrationFactory.CreateTableMigration(this);
        }
    }

    public class CreateColumnMigration : Migration
    {
        public ColumnSchema Column { get; private set; }

        public CreateColumnMigration(ColumnSchema column)
        {
            Column = column;
        }


        public dynamic GetGenerator(MigrationFactory migrationFactory)
        {
            return migrationFactory.CreateColumnGenerator(this);
        }
    }
}