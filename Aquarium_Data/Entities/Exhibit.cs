using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium_Data.Entities
{
    public class Exhibit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100000, MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public ICollection<TanksExhibit> TanksExhibits { get; set; } = new List<TanksExhibit>();
    }
}
