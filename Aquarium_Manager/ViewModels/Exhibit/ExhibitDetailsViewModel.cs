namespace Aquarium_Manager.ViewModels.Exhibit
{
    public class ExhibitDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<string> TankNames { get; set; } = new();
    }
}
