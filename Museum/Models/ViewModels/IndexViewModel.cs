using Museum.Models.DbModels;
using Museum.Models.DtoModels;

namespace Museum.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<ExhibitCategory> Category { get; set; }
        public List<ExhibitDto> Exhibits { get; set; }
    }
}
