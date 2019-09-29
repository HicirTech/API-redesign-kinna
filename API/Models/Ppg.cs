using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Ppg
    {
        //if want insert key manually, ↓↓↓ enable next line
        // after that redo the migrationm and update databse
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int? id { get; set; }

        [Required]
        public DateTime time { get; set; }

        [Required]
        public int heartRate { get; set; }

        public float? hrConfidence { get; set; }
        public int? greenCount1 { get; set; }
        public int? greenCount2 { get; set; }
        public float? xAccel { get; set; }
        public float? yAccel { get; set; }
        public float? zAccel { get; set; }
    }
}
