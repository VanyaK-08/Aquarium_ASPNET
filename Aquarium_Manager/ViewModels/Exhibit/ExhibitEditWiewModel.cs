using System.ComponentModel.DataAnnotations;

namespace Aquarium_Manager.ViewModels.Exhibit
{
    public class ExhibitEditWiewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на експозицията е задължително")]
        [StringLength(100, ErrorMessage = "Името може да бъде максимум 100 символа")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описанието е задължително")]
        [StringLength(100000, ErrorMessage = "Описанието е твърде дълго")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Аквариуми")]
        public List<int> SelectedTankIds { get; set; } = new();
    }
}
