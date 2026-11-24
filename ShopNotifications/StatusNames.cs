namespace ShopNotifications
{
    static class StatusNames
    {
        public static string Get(OrderStatus status)
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
