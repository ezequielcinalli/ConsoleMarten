public class WarehouseProductWriteModel
{
    public Guid Id { get; set; }
    public int QuantityOnHand { get; set; }

    public void Apply(ProductShipped evnt)
    {
        this.Id = evnt.Id;
        this.QuantityOnHand -= evnt.Quantity;
    }

    public void Apply(ProductReceived evnt)
    {
        this.Id = evnt.Id;
        this.QuantityOnHand += evnt.Quantity;
    }

    public void Apply(InventoryAdjusted evnt)
    {
        this.Id = evnt.Id;
        this.QuantityOnHand += evnt.Quantity;
    }
}