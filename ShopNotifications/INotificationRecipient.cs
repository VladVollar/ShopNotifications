namespace ShopNotifications
{
    interface INotificationRecipient
    {
        void Notify(Order order);
    }
}
