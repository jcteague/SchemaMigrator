using System.Linq;
using FluentMigrator.Expressions;
using FluentMigrator.Model;
using SchemaBuilder;

namespace SchemaMigration.FluentMigrator
{
    public class FluentMigratorMigrationFactory : MigrationFactory
    {
        public dynamic CreateTableMigration(CreateTableMigration createTableMigration)
        {
            var create_table_expr = new CreateTableExpression
                                        {
                                            TableName = createTableMigration.TableName,
                                            SchemaName = createTableMigration.SchemaName,
                                        };
            create_table_expr.Columns = createTableMigration.Colunns.Select(build_column_definition).ToList();

            return create_table_expr;
        }

        ColumnDefinition build_column_definition(CreateColumnMigration columnMigration)
        {
            var column = columnMigration.Column;
            return new ColumnDefinition
                       {
                           TableName = column.TableName,
                           Name = column.ColumnName,
                           DefaultValue = column.DefaultValue,
                           IsForeignKey = column.IsForiegnKey,
                           IsIdentity = column.IsIdentity,
                           IsNullable = column.IsNullable,
                           IsPrimaryKey = column.IsPrimaryKey,
                           Precision = column.Precision,
                           PrimaryKeyName = column.PrimaryKeyName,
                           Size = column.Size,
                           Type = column.DatabaseType

                       };
            
        }

        public dynamic CreateColumnGenerator(CreateColumnMigration createColumnMigration)
        {
            var column_migration_expr = new CreateColumnExpression
                                            {
                                                Column = build_column_definition(createColumnMigration)
                                            };

            return column_migration_expr;

        }
    }
}