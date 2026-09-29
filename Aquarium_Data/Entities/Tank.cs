using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium_Data.Entities
{
    public class Tank
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 100000)]
        public double Volume { get; set; }

        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public ICollection<Fish> Fish { get; set; } = new List<Fish>();
    }
}
