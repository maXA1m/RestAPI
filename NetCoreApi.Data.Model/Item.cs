namespace NetCoreApi.Data.Model
{
    public class Item : EntityBase
    {
        public string Name { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
