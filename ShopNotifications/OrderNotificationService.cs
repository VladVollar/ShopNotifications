namespace ShopNotifications
{
    class OrderNotificationService
    {
        private readonly INotificationRecipient client = new ClientNotifier();
        private readonly INotificationRecipient warehouse = new WarehouseNotifier();
        private readonly INotificationRecipient accounting = new AccountingNotifier();
        private readonly INotificationRecipient delivery = new DeliveryNotifier();

        public void Notify(Order order)
        {
            client.Notify(order);
            warehouse.Notify(order);
            accounting.Notify(order);
            delivery.Notify(order);
        }
    }
}
