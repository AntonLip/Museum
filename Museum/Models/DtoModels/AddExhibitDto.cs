using Museum.Models.Interfaces;

namespace Museum.Models.DtoModels
{
    public class AddExhibitDto : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descrition { get; set; }
        public IFormFile PhotoPath { get; set; }
        public IFormFile IconPath { get; set; }
        public Guid CategoryId { get; set; }
    }
}
