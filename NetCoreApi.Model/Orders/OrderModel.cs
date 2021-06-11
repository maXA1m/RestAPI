using System;

namespace NetCoreApi.Model.Orders
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
