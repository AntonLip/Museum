using Museum.Models.Interfaces;

namespace Museum.Models.DbModels
{
    public class Room : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
