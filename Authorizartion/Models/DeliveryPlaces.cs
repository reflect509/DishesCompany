using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesCompany
{
    public class DeliveryPlaces
    {
        [Key] public int Delivery_place_id { get; set; }
        public string Delivery_place_address { get; set; }

        public List<Orders> Order_entities { get; set; }
    }
}
