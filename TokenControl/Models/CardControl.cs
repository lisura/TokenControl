using System;
using System.ComponentModel.DataAnnotations;

namespace TokenControl.Models
{
    public class CardControl
    {
        public int Id { get; set; }

        [Required]
        public int? CustomerId { get; set; }

        [Required]
        [Range(0, 9999999999999999)]
        public long? Cardnumber { get; set; }

        [Required]
        [Range(0, 99999)]
        public int CVV { get; set; }

        public DateTime RegistrationDate { get; set; }

    }
}
