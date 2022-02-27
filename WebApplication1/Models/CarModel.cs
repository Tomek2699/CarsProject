using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CarModel
    {
        [Key]
        public int CarId { get; set; }
        [Required(ErrorMessage = "Proszę podać markę auta")]
        [MaxLength(30)]
        public string Mark { get; set; }
        [Required(ErrorMessage = "Proszę podać model")]
        [MaxLength(30)]
        public string Model { get; set; }
        [Required(ErrorMessage = "Proszę podać kolor")]
        [MaxLength(20)]
        public string Color { get; set; }
        [Required(ErrorMessage = "Proszę podać rodzaj paliwa")]
        [MaxLength(20)]
        public string FuelType { get; set; }
        [Required(ErrorMessage = "Proszę podać przebieg auta")]
        public int Mileage { get; set; }

    }
}
