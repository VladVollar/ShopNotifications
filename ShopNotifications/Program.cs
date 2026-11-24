using System;
using System.Text;

namespace ShopNotifications
{
    class Program
    {
        static readonly OrderNotificationService notifications = new OrderNotificationService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Order firstOrder = new Order(1001);
            Console.WriteLine("Замовлення №1001");
            ChangeStatus(firstOrder, OrderStatus.Confirmed);
            ChangeStatus(firstOrder, OrderStatus.Paid);
            ChangeStatus(firstOrder, OrderStatus.ReadyForShipping);
            ChangeStatus(firstOrder, OrderStatus.Delivered);
            ChangeStatus(firstOrder, OrderStatus.Delivered);

            Order secondOrder = new Order(1002);
            Console.WriteLine("Замовлення №1002");
            ChangeStatus(secondOrder, OrderStatus.Confirmed);
            ChangeStatus(secondOrder, OrderStatus.Canceled);
        }

        static void ChangeStatus(Order order, OrderStatus newStatus)
        {
            if (order.Status == newStatus)
                return;

            order.Status = newStatus;
            Console.WriteLine("Новий стан: " + StatusNames.Get(newStatus));
            notifications.Notify(order);
            Console.WriteLine();
        }
    }
}
