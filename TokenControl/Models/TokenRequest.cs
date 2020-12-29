using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenControl.Models
{
    public class TokenRequest
    {
        [Required]
        public int? CustomerId { get; set; }

        [Required]
        public int? CardId { get; set; }

        [Required]
        public long? Token { get; set; }

        [Required]
        [Range(0, 99999)]
        public int CVV { get; set; }
    }
}
