using System;

namespace ShopNotifications
{
    class AccountingNotifier : IOrderObserver
    {
        public void Update(Order order)
        {
            if (order.Status == OrderStatus.Paid)
                Console.WriteLine("[Бухгалтерія] Оплачено замовлення №" + order.Id);
            else if (order.Status == OrderStatus.Canceled)
                Console.WriteLine("[Бухгалтерія] Скасовано замовлення №" + order.Id);
        }
    }
}
