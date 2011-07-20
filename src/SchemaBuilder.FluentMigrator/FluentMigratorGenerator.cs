using System;
using System.Collections.Generic;
using FluentMigrator;
using FluentMigrator.Infrastructure;
using IMigrationGenerator = SchemaBuilder.IMigrationGenerator;
using Migration = SchemaBuilder.Migration;

namespace SchemaMigration.FluentMigrator
{
    public class FluentMigratorGenerator : IMigrationGenerator
    {
        

        public IEnumerable<dynamic> GenerateMigrations(IEnumerable<Migration> migrations)
        {
            var migration_expressions = new List<dynamic>();
            foreach (var migration in migrations)
            {
                migration_expressions.Add(migration.GetGenerator(new FluentMigratorMigrationFactory()));
            }
            return migration_expressions;
        }

        
    }
}