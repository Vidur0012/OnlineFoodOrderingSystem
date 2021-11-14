using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderingSystem.Models
{
    public class Payment
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(1000000000,9999999999,ErrorMessage ="please enter valied mob")]
        public int Mob { get; set; }

    }
}
