using Museum.Models.Interfaces;

namespace Museum.Models.DbModels
{
    public class Ticket : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserId { get; set; }
        public bool IsChild { get; set; }
        public DateTime VisitTime { get; set; }
        public float Price { get; set; }
    }
}
