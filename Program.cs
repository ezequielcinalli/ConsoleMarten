const string connectionString =
    "PORT = 5432; HOST = localhost; TIMEOUT = 15; POOLING = True; DATABASE = 'warehouse'; PASSWORD = 'postgres'; USER ID = 'postgres'";

var documentStore = DocumentStore.For(options =>
{
    options.Connection(connectionString);

    options.Projections.Add<WarehouseProductProjection>(ProjectionLifecycle.Inline);
});

var id = Guid.NewGuid();

var warehouseRepository = new WarehouseRepository(documentStore);
var warehouseProductReadModel = warehouseRepository.Get(id);

DemoConsole.WriteWithColour($"{warehouseProductReadModel?.QuantityOnHand ?? 0} items of stock in the warehouse for {id}");

var handler = new WarehouseProductHandler(id, documentStore);
handler.ReceiveProduct(100);

DemoConsole.WriteWithColour($"Received 100 items of stock into the warehouse for {id}");

handler.ShipProduct(10);

DemoConsole.WriteWithColour($"Shipped 10 items of stock out of the warehouse for {id}");

handler.AdjustInventory(5, "Ordered too many");

DemoConsole.WriteWithColour($"Found 5 items of stock hiding in the warehouse for {id} and have adjusted the stock count");

warehouseProductReadModel = warehouseRepository.Get(id);

DemoConsole.WriteWithColour($"{warehouseProductReadModel.QuantityOnHand} items of stock in the warehouse for {warehouseProductReadModel.Id}");








