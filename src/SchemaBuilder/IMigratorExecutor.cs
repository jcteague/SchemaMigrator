using System.Collections.Generic;

namespace SchemaBuilder
{
    public interface IMigratorExecutor
    {
        void ExecuteMigrations(IEnumerable<dynamic> generatedMigrations);
    }
}