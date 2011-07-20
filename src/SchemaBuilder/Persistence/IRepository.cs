namespace SchemaBuilder.Persistence
{
    public interface IRepository
    {
        DbSchema GetCurrentSchema();
    }
}