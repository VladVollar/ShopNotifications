namespace ShopNotifications
{
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
}
