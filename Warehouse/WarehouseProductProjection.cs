public class WarehouseProductProjection : SingleStreamAggregation<WarehouseProductReadModel>
{
    public WarehouseProductProjection()
    {
        this.ProjectEvent<ProductShipped>(this.Apply);
        this.ProjectEvent<ProductReceived>(this.Apply);
        this.ProjectEvent<InventoryAdjusted>(this.Apply);
    }


    public void Apply(WarehouseProductReadModel readModel, ProductShipped evnt)
    {
        readModel.QuantityOnHand -= evnt.Quantity;
    }

    public void Apply(WarehouseProductReadModel readModel, ProductReceived evnt)
    {
        readModel.QuantityOnHand += evnt.Quantity;
    }

    public void Apply(WarehouseProductReadModel readModel, InventoryAdjusted evnt)
    {
        readModel.QuantityOnHand += evnt.Quantity;
    }
}