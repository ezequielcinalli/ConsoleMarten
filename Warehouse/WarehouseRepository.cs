public class WarehouseRepository
{
    private readonly IDocumentStore documentStore;

    public WarehouseRepository(IDocumentStore documentStore)
    {
        this.documentStore = documentStore;
    }

    public WarehouseProductReadModel Get(Guid id)
    {
        using var session = this.documentStore.OpenSession();

        var doc = session.Query<WarehouseProductReadModel>()
            .SingleOrDefault(x => x.Id == id);

        return doc;
    }
}