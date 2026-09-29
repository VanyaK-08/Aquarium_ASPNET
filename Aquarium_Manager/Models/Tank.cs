using Aquarium_Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Aquarium_Manager.Models
{
    public class Tank
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на аквариума е задължително")]
        [StringLength(100, ErrorMessage = "Максимум 100 символа")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обемът е задължителен")]
        [Range(1, 100000, ErrorMessage = "Обемът трябва да е между 1 и 100000 литра")]
        public double Volume { get; set; }

        [Required(ErrorMessage = "Местоположението е задължително")]
        [StringLength(100, ErrorMessage = "Максимум 100 символа")]
        public string Location { get; set; }

        public ICollection<Fish> Fish { get; set; } = new List<Fish>();
    }
}
