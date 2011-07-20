using FizzWare.NBuilder;
using NHibernate.Cfg;
using NUnit.Framework;
using SchemaBuilder;
using Tests.TestDatabase;

namespace AutoMigrator.Tests.NhiberanteSchemaBuilder
{
    [TestFixture]
    public class NhibernateBaseTester
    {
        
        protected IConfiguration config;

        [SetUp]
        public void SetUp()
        {
            var nh_config = NHiberanteConfiguration.BuildConfiguration(configuration: m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>());
            config = Builder<IConfiguration>.CreateNew().With(x => x.SchemaEngineOptions.Configuration = nh_config).Build();   
        }
        [TestFixtureSetUp]
        public void FixtureSetup()
        {

            
        }
    }
}