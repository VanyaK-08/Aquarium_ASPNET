using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium_Data.Entities
{
    public class Fish
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Species { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Age { get; set; }

        [ForeignKey("Tank")]
        public int TankId { get; set; }

        public Tank? Tank { get; set; }
    }
}
