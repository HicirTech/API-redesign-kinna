using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Temp
    {
        [Key]
        public int id { get; set; }
        
        [Required]
        public float temp { get; set; }

        [Required]
        public DateTime time { get; set; }
    }
}
