namespace ShopNotifications
{
    class OrderNotificationService
    {
        private readonly ClientNotifier client = new ClientNotifier();
        private readonly WarehouseNotifier warehouse = new WarehouseNotifier();
        private readonly AccountingNotifier accounting = new AccountingNotifier();
        private readonly DeliveryNotifier delivery = new DeliveryNotifier();

        public void Notify(Order order)
        {
            client.Notify(order);
            warehouse.Notify(order);
            accounting.Notify(order);
            delivery.Notify(order);
        }
    }
}
