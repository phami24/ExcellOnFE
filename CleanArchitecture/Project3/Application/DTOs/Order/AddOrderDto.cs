using Application.DTOs.OrderDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class AddOrderDto
    {

        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public double OrderTotal { get; set; }

        public int ClientId { get; set; }
    }
}
