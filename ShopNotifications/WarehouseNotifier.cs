using System;

namespace ShopNotifications
{
    class WarehouseNotifier : IOrderObserver
    {
        public void Update(Order order)
        {
            if (order.Status == OrderStatus.Confirmed)
                Console.WriteLine("[Склад] Підготувати замовлення №" + order.Id);
            else if (order.Status == OrderStatus.Canceled)
                Console.WriteLine("[Склад] Скасовано замовлення №" + order.Id);
        }
    }
}
