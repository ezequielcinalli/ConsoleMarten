public class WarehouseProductHandler
{
    private readonly Guid id;
    private readonly IDocumentStore documentStore;

    public WarehouseProductHandler(Guid id, IDocumentStore documentStore)
    {
        this.id = id;
        this.documentStore = documentStore;
    }

    public void ShipProduct(int quantity)
    {
        using var session = this.documentStore.OpenSession();

        var warehouseProduct = session.Events.AggregateStream<WarehouseProductWriteModel>(this.id);

        if (quantity > warehouseProduct.QuantityOnHand)
        {
            throw new InvalidDomainException("Ah... we don't have enough product to ship?");
        }

        session.Events.Append(this.id, new ProductShipped(this.id, quantity, DateTime.UtcNow));
        session.SaveChanges();
    }

    public void ReceiveProduct(int quantity)
    {
        using var session = this.documentStore.OpenSession();

        var warehouseProduct = session.Events.AggregateStream<WarehouseProductWriteModel>(this.id);

        session.Events.Append(this.id, new ProductReceived(this.id, quantity, DateTime.UtcNow));
        session.SaveChanges();
    }

    public void AdjustInventory(int quantity, string reason)
    {
        using var session = this.documentStore.OpenSession();

        var warehouseProduct = session.Events.AggregateStream<WarehouseProductWriteModel>(this.id);

        if (warehouseProduct.QuantityOnHand + quantity < 0)
        {
            throw new InvalidDomainException("Cannot adjust to a negative quantity on hand.");
        }

        session.Events.Append(this.id, new InventoryAdjusted(this.id, quantity, reason, DateTime.UtcNow));
        session.SaveChanges();
    }
}