using System;

namespace ShopNotifications
{
    class ClientNotifier : IOrderObserver
    {
        public void Update(Order order)
        {
            Console.WriteLine("[Клієнт] Замовлення №" + order.Id + ": " + StatusNames.Get(order.Status));
        }
    }
}
