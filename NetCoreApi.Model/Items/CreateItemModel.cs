using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreApi.Model.Items
{
    public class CreateItemModel
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AvailableQuantity { get; set; }
    }
}
