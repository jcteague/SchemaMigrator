using Moq;
using NUnit.Framework;

namespace AutoMigrator.Tests.SchemaBuilder
{
    [TestFixture]
    public abstract class AutoMockingTest<T> where T : class
    {
        protected MockRepository m;
        protected AutoMockContainer container;
        protected T sut;

        [SetUp]
        public void SetUp()
        {
            m = new MockRepository(MockBehavior.Loose);
            container = new AutoMockContainer(m);
            sut = container.Create<T>();
        }
        protected  Mock<K> For<K>() where K : class
        {
            return container.GetMock<K>();
        }
    }
}