using System;

namespace ShopNotifications
{
    class DeliveryNotifier
    {
        public void Notify(Order order)
        {
            if (order.Status == OrderStatus.ReadyForShipping)
                Console.WriteLine("[Служба доставки] Забрати замовлення №" + order.Id);
        }
    }
}
