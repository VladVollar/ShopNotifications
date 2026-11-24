using System;
using System.Text;

namespace ShopNotifications
{
    enum OrderStatus
    {
        New,
        Confirmed,
        Paid,
        ReadyForShipping,
        Delivered,
        Canceled
    }

    class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }

        public Order(int id)
        {
            Id = id;
            Status = OrderStatus.New;
        }
    }

    class Program
    {
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
            Console.WriteLine("Новий стан: " + GetStatusName(newStatus));
            NotifyClient(order);

            if (newStatus == OrderStatus.Confirmed)
                NotifyWarehouse(order, false);

            if (newStatus == OrderStatus.Paid)
                NotifyAccounting(order, false);

            if (newStatus == OrderStatus.ReadyForShipping)
                NotifyDelivery(order);

            if (newStatus == OrderStatus.Canceled)
            {
                NotifyWarehouse(order, true);
                NotifyAccounting(order, true);
            }

            Console.WriteLine();
        }

        static void NotifyClient(Order order)
        {
            Console.WriteLine("[Клієнт] Замовлення №" + order.Id + ": " + GetStatusName(order.Status));
        }

        static void NotifyWarehouse(Order order, bool canceled)
        {
            if (canceled)
                Console.WriteLine("[Склад] Скасовано замовлення №" + order.Id);
            else
                Console.WriteLine("[Склад] Підготувати замовлення №" + order.Id);
        }

        static void NotifyAccounting(Order order, bool canceled)
        {
            if (canceled)
                Console.WriteLine("[Бухгалтерія] Скасовано замовлення №" + order.Id);
            else
                Console.WriteLine("[Бухгалтерія] Оплачено замовлення №" + order.Id);
        }

        static void NotifyDelivery(Order order)
        {
            Console.WriteLine("[Служба доставки] Забрати замовлення №" + order.Id);
        }

        static string GetStatusName(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.New: return "Нове";
                case OrderStatus.Confirmed: return "Підтверджене";
                case OrderStatus.Paid: return "Оплачене";
                case OrderStatus.ReadyForShipping: return "Готове до відправлення";
                case OrderStatus.Delivered: return "Доставлене";
                case OrderStatus.Canceled: return "Скасоване";
                default: return "Невідомий стан";
            }
        }
    }
}
