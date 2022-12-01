using Museum.Models.Interfaces;

namespace Museum.Models.DtoModels
{
    public class ExhibitDto : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descrition { get; set; }
        public string PhotoPath { get; set; }
        public string IconPath { get; set; }

        public string ExhibitCategoryName { get; set; }
    }
}
