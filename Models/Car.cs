using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car_Dealership.Models
{
    public class Car
    {
        public int ID { get; set; }
        [Required,StringLength(150,MinimumLength =3)]
        [Display(Name ="Car Model")]
        public string Name { get; set; }
        [Display(Name = "Car Make")]
        [Required, StringLength(150, MinimumLength = 3)]
        public string Make { get; set; } 
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        [Range(1, 300)]

        public decimal MaxSpeed { get; set; }

        [DataType(DataType.Date)]
        public DateTime ListingDate { get; set; }

        public int SellerID { get; set; }

        public Seller Seller { get; set; }  

        public ICollection<CarCategory> CarCategories { get; set; }

    }
}
