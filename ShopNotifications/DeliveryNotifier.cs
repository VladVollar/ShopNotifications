using System;

namespace ShopNotifications
{
    class DeliveryNotifier : IOrderObserver
    {
        public void Update(Order order)
        {
            if (order.Status == OrderStatus.ReadyForShipping)
                Console.WriteLine("[Служба доставки] Забрати замовлення №" + order.Id);
        }
    }
}
