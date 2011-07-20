using System.Collections.Generic;

namespace SchemaBuilder
{
    public interface IMigrationGenerator
    {
        IEnumerable<dynamic> GenerateMigrations(IEnumerable<Migration> migrations);
        
    }

    public interface CreateTableMigrationGenerator
    {
        
    }

    public interface MigrationGeneratorFactory
    {
        
    }
}