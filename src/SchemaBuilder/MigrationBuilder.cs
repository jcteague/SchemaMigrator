using System.Collections.Generic;
using System.Linq;

namespace SchemaBuilder
{
    public class MigrationBuilder
    {
        public Migration[] GenerateSchemaDiff(DbSchema currentSchema, DbSchema newSchema)
        {
            var migration = new List<Migration>();
            var new_tables = get_new_tables(currentSchema.Tables, newSchema.Tables);
            migration.AddRange(create_tables(new_tables));

            return migration.ToArray();
        }

        

        IEnumerable<TableSchema> get_new_tables(IEnumerable<TableSchema> currentSchemaTables, IEnumerable<TableSchema> newSchemaTables)
        {
            foreach (var new_table in newSchemaTables)
            {
                if (!currentSchemaTables.Contains(new_table))
                    yield return new_table;
            }
        }

        IEnumerable<Migration> create_tables(IEnumerable<TableSchema> newTables)
        {
            foreach (var table in newTables)
            {
                var table_migration = new CreateTableMigration() {TableName = table.TableName};
                table_migration.Colunns = create_column_migrations(table);
                yield return table_migration;
            }
        }

        IEnumerable<CreateColumnMigration> create_column_migrations(TableSchema table)
        {
            return table.Columns.Select(col => new CreateColumnMigration(col));
        }
    }
}