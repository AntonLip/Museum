using Museum.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museum.Models.DbModels
{
    public class Exhibit : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descrition { get; set; }
        public string PhotoPath { get; set; }
        public string IconPath { get; set; }

        [ForeignKey("ExhibitCategory")]
        public Guid CategoryId { get; set; }
        public ExhibitCategory ExhibitCategory { get; set; }
    }
}
