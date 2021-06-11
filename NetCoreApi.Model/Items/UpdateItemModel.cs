using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreApi.Model.Items
{
    public class UpdateItemModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AvailableQuantity { get; set; }
    }
}
