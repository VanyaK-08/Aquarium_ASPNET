using System.ComponentModel.DataAnnotations;

namespace Aquarium_Manager.ViewModels.Fish
{
    public class FishEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на рибата е задължително")]
        [StringLength(100, ErrorMessage = "Името може да бъде максимум 100 символа")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Видът на рибата е задължителен")]
        [StringLength(100, ErrorMessage = "Видът може да бъде максимум 100 символа")]
        public string Species { get; set; }

        [Required(ErrorMessage = "Възрастта е задължителна")]
        [Range(1, 100, ErrorMessage = "Възрастта трябва да бъде между 1 и 100 години")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Трябва да изберете аквариум")]
        [Display(Name = "Аквариум")]
        public int? TankId { get; set; }
    }
}
