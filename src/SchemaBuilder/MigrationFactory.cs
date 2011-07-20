namespace SchemaBuilder
{
    public interface MigrationFactory
    {
        dynamic CreateTableMigration(CreateTableMigration createTableMigration);
        dynamic CreateColumnGenerator(CreateColumnMigration createColumnMigration);
    }
}