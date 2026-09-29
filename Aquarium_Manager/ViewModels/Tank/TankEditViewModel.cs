using System.ComponentModel.DataAnnotations;

namespace Aquarium_Manager.ViewModels.Tank
{
    public class TankEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на аквариума е задължително")]
        [StringLength(100, ErrorMessage = "Името може да бъде максимум 100 символа")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обемът е задължителен")]
        [Range(1, 100000, ErrorMessage = "Обемът трябва да бъде между 1 и 100000 литра")]
        public double Volume { get; set; }

        [Required(ErrorMessage = "Местоположението е задължително")]
        [StringLength(100, ErrorMessage = "Местоположението може да бъде максимум 100 символа")]
        public string Location { get; set; }
    }
}
