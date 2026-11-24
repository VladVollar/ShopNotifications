using System;

namespace ShopNotifications
{
    class ClientNotifier : INotificationRecipient
    {
        public void Notify(Order order)
        {
            Console.WriteLine("[Клієнт] Замовлення №" + order.Id + ": " + StatusNames.Get(order.Status));
        }
    }
}
