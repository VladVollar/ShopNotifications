using System;
using System.Collections.Generic;

namespace ShopNotifications
{
    class Order
    {
        private readonly List<IOrderObserver> observers = new List<IOrderObserver>();

        public int Id { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(int id)
        {
            Id = id;
            Status = OrderStatus.New;
        }

        public void Subscribe(IOrderObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));

            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void Unsubscribe(IOrderObserver observer)
        {
            observers.Remove(observer);
        }

        public bool ChangeStatus(OrderStatus newStatus)
        {
            if (Status == newStatus)
                return false;

            Status = newStatus;
            foreach (IOrderObserver observer in observers.ToArray())
                observer.Update(this);

            return true;
        }
    }
}
