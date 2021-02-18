using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Dealership.Models
{
    public class Seller
    {
        public int ID { get; set; }
        public string SellerName { get;set; }
        public string SellerAdress { get; set; }

        public ICollection<Car> Cars { get; set; }  
    }
}
