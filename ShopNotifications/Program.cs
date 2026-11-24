using System;
using System.Text;

namespace ShopNotifications
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            RunDemo();
        }

        internal static void RunDemo()
        {
            Order firstOrder = new Order(1001);
            SubscribeStandardRecipients(firstOrder);
            Console.WriteLine("Замовлення №1001");
            ChangeStatus(firstOrder, OrderStatus.Confirmed);
            ChangeStatus(firstOrder, OrderStatus.Paid);
            ChangeStatus(firstOrder, OrderStatus.ReadyForShipping);
            ChangeStatus(firstOrder, OrderStatus.Delivered);
            ChangeStatus(firstOrder, OrderStatus.Delivered);

            Order secondOrder = new Order(1002);
            SubscribeStandardRecipients(secondOrder);
            Console.WriteLine("Замовлення №1002");
            ChangeStatus(secondOrder, OrderStatus.Confirmed);
            ChangeStatus(secondOrder, OrderStatus.Canceled);
        }

        static void SubscribeStandardRecipients(Order order)
        {
            order.Subscribe(new ClientNotifier());
            order.Subscribe(new WarehouseNotifier());
            order.Subscribe(new AccountingNotifier());
            order.Subscribe(new DeliveryNotifier());
        }

        static void ChangeStatus(Order order, OrderStatus newStatus)
        {
            if (order.Status == newStatus)
                return;

            Console.WriteLine("Новий стан: " + StatusNames.Get(newStatus));
            order.ChangeStatus(newStatus);
            Console.WriteLine();
        }
    }
}
