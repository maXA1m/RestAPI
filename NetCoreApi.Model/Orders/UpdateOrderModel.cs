using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreApi.Model.Orders
{
    public class UpdateOrderModel
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
