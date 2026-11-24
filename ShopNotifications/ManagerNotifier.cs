using System;

namespace ShopNotifications
{
    class ManagerNotifier : IOrderObserver
    {
        public void Update(Order order)
        {
            Console.WriteLine("[Менеджер] Змінився стан замовлення №" + order.Id + ": " + StatusNames.Get(order.Status));
        }
    }
}
