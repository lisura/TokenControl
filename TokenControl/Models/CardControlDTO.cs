using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenControl.Models
{
    public class CardControlDTO
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public long Token { get; set; }
    }
}
