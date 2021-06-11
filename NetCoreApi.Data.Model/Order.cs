using System;

namespace NetCoreApi.Data.Model
{
    public class Order : EntityBase
    {
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
