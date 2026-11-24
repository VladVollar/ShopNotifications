using System;

namespace ShopNotifications
{
    class AccountingNotifier
    {
        public void Notify(Order order)
        {
            if (order.Status == OrderStatus.Paid)
                Console.WriteLine("[Бухгалтерія] Оплачено замовлення №" + order.Id);
            else if (order.Status == OrderStatus.Canceled)
                Console.WriteLine("[Бухгалтерія] Скасовано замовлення №" + order.Id);
        }
    }
}
