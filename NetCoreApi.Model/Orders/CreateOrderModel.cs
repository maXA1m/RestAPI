using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreApi.Model.Orders
{
    public class CreateOrderModel
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
