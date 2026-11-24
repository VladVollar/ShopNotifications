using System;
using System.Text;

namespace ShopNotifications
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (args.Length > 0 && args[0] == "--test")
                return SelfTests.Run();
            if (args.Length > 0 && args[0] == "--extended")
                RunExtended();
            else
                RunDemo();
            return 0;
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

        internal static void RunExtended()
        {
            Order order = new Order(2001);
            ManagerNotifier manager = new ManagerNotifier();
            order.Subscribe(new ClientNotifier());
            order.Subscribe(manager);

            Console.WriteLine("Замовлення №2001");
            ChangeStatus(order, OrderStatus.Confirmed);

            order.Unsubscribe(manager);
            ChangeStatus(order, OrderStatus.Paid);
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
