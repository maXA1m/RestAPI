using System;

namespace NetCoreApi.Model.Items
{
    public class ItemModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
