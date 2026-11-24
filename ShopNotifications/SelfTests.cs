using System;
using System.IO;

namespace ShopNotifications
{
    static class SelfTests
    {
        private sealed class CountingObserver : IOrderObserver
        {
            public int Calls { get; private set; }
            public void Update(Order order)
            {
                Calls++;
            }
        }

        public static int Run()
        {
            int failures = 0;
            Check("Початковий вивід не змінився", TestBaseline, ref failures);
            Check("Повторний стан не надсилає сповіщення", TestDuplicateStatus, ref failures);
            Check("Відписка припиняє сповіщення", TestUnsubscribe, ref failures);
            Check("Повторна підписка не дублює повідомлення", TestDuplicateSubscription, ref failures);
            Check("Кожен відділ отримує лише потрібний стан", TestRecipientFilters, ref failures);
            Check("Новий отримувач підключається без змін Order", TestOptionalManager, ref failures);
            Console.WriteLine(failures == 0 ? "Всі перевірки пройдено." : "Помилок: " + failures);
            return failures == 0 ? 0 : 1;
        }

        private static void Check(string name, Action test, ref int failures)
        {
            try
            {
                test();
                Console.WriteLine("OK: " + name);
            }
            catch (Exception error)
            {
                failures++;
                Console.WriteLine("ПОМИЛКА: " + name + " — " + error.Message);
            }
        }

        private static string Capture(Action action)
        {
            TextWriter original = Console.Out;
            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    Console.SetOut(writer);
                    action();
                }
                finally
                {
                    Console.SetOut(original);
                }
                return writer.ToString().Replace("\r\n", "\n");
            }
        }

        private static void Require(bool condition, string message)
        {
            if (!condition)
                throw new Exception(message);
        }

        private static void TestBaseline()
        {
            string[] lines = new string[]
            {
                "Замовлення №1001",
                "Новий стан: Підтверджене",
                "[Клієнт] Замовлення №1001: Підтверджене",
                "[Склад] Підготувати замовлення №1001",
                "",
                "Новий стан: Оплачене",
                "[Клієнт] Замовлення №1001: Оплачене",
                "[Бухгалтерія] Оплачено замовлення №1001",
                "",
                "Новий стан: Готове до відправлення",
                "[Клієнт] Замовлення №1001: Готове до відправлення",
                "[Служба доставки] Забрати замовлення №1001",
                "",
                "Новий стан: Доставлене",
                "[Клієнт] Замовлення №1001: Доставлене",
                "",
                "Замовлення №1002",
                "Новий стан: Підтверджене",
                "[Клієнт] Замовлення №1002: Підтверджене",
                "[Склад] Підготувати замовлення №1002",
                "",
                "Новий стан: Скасоване",
                "[Клієнт] Замовлення №1002: Скасоване",
                "[Склад] Скасовано замовлення №1002",
                "[Бухгалтерія] Скасовано замовлення №1002",
                ""
            };
            string expected = string.Join("\n", lines) + "\n";
            string actual = Capture(() => Program.RunDemo());
            Require(actual == expected, "вивід відрізняється від вихідної програми");
        }

        private static void TestDuplicateStatus()
        {
            Order order = new Order(10);
            CountingObserver observer = new CountingObserver();
            order.Subscribe(observer);
            Require(order.ChangeStatus(OrderStatus.Confirmed), "перша зміна не виконалась");
            Require(!order.ChangeStatus(OrderStatus.Confirmed), "повторна зміна повинна повернути false");
            Require(observer.Calls == 1, "отримано зайве сповіщення");
        }

        private static void TestUnsubscribe()
        {
            Order order = new Order(11);
            CountingObserver observer = new CountingObserver();
            order.Subscribe(observer);
            order.ChangeStatus(OrderStatus.Confirmed);
            order.Unsubscribe(observer);
            order.ChangeStatus(OrderStatus.Paid);
            Require(observer.Calls == 1, "відписаний спостерігач викликався");
        }

        private static void TestDuplicateSubscription()
        {
            Order order = new Order(12);
            CountingObserver observer = new CountingObserver();
            order.Subscribe(observer);
            order.Subscribe(observer);
            order.ChangeStatus(OrderStatus.Confirmed);
            Require(observer.Calls == 1, "подвійна підписка призвела до дублювання");
        }

        private static void TestRecipientFilters()
        {
            Order order = new Order(13);
            order.Subscribe(new ClientNotifier());
            order.Subscribe(new WarehouseNotifier());
            order.Subscribe(new AccountingNotifier());
            order.Subscribe(new DeliveryNotifier());
            string output = Capture(() => order.ChangeStatus(OrderStatus.Paid));
            Require(output.Contains("[Клієнт]"), "клієнт не повідомлений");
            Require(output.Contains("[Бухгалтерія]"), "бухгалтерія не повідомлена");
            Require(!output.Contains("[Склад]"), "склад отримав зайве сповіщення");
            Require(!output.Contains("[Служба доставки]"), "доставка отримала зайве сповіщення");
        }

        private static void TestOptionalManager()
        {
            string output = Capture(() => Program.RunExtended());
            string marker = "[Менеджер]";
            int first = output.IndexOf(marker, StringComparison.Ordinal);
            Require(first >= 0, "менеджер не отримав повідомлення");
            Require(output.IndexOf(marker, first + marker.Length, StringComparison.Ordinal) < 0,
                "менеджер продовжує отримувати повідомлення після відписки");
        }
    }
}
