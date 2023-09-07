﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Indo.DeliveryOrderDetails
{
    public class DeliveryOrderDetailCreateDto
    {

        [Required]
        public Guid DeliveryOrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public float Quantity { get; set; }
    }
}
