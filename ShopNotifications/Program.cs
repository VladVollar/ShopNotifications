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
            {
                return;
            }

            order.Status = newStatus;
            string statusName = GetStatusName(newStatus);

            Console.WriteLine("Новий стан: " + statusName);
            Console.WriteLine("[Клієнт] Замовлення №" + order.Id + ": " + statusName);

            if (newStatus == OrderStatus.Confirmed)
            {
                Console.WriteLine("[Склад] Підготувати замовлення №" + order.Id);
            }

            if (newStatus == OrderStatus.Paid)
            {
                Console.WriteLine("[Бухгалтерія] Оплачено замовлення №" + order.Id);
            }

            if (newStatus == OrderStatus.ReadyForShipping)
            {
                Console.WriteLine("[Служба доставки] Забрати замовлення №" + order.Id);
            }

            if (newStatus == OrderStatus.Canceled)
            {
                Console.WriteLine("[Склад] Скасовано замовлення №" + order.Id);
                Console.WriteLine("[Бухгалтерія] Скасовано замовлення №" + order.Id);
            }

            Console.WriteLine();
        }

        static string GetStatusName(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.New:
                    return "Нове";
                case OrderStatus.Confirmed:
                    return "Підтверджене";
                case OrderStatus.Paid:
                    return "Оплачене";
                case OrderStatus.ReadyForShipping:
                    return "Готове до відправлення";
                case OrderStatus.Delivered:
                    return "Доставлене";
                case OrderStatus.Canceled:
                    return "Скасоване";
                default:
                    return "Невідомий стан";
            }
        }
    }
}
