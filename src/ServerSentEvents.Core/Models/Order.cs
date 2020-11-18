using System;
using System.ComponentModel.DataAnnotations;

namespace ServerSentEvents.Core.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
    }

}
