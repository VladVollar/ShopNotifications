using System;

namespace ShopNotifications
{
    class OrderNotificationService
    {
        public void Notify(Order order)
        {
            Console.WriteLine("[Клієнт] Замовлення №" + order.Id + ": " + StatusNames.Get(order.Status));

            if (order.Status == OrderStatus.Confirmed)
                Console.WriteLine("[Склад] Підготувати замовлення №" + order.Id);

            if (order.Status == OrderStatus.Paid)
                Console.WriteLine("[Бухгалтерія] Оплачено замовлення №" + order.Id);

            if (order.Status == OrderStatus.ReadyForShipping)
                Console.WriteLine("[Служба доставки] Забрати замовлення №" + order.Id);

            if (order.Status == OrderStatus.Canceled)
            {
                Console.WriteLine("[Склад] Скасовано замовлення №" + order.Id);
                Console.WriteLine("[Бухгалтерія] Скасовано замовлення №" + order.Id);
            }
        }
    }
}
