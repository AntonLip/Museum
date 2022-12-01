using AutoMapper;
using Museum.Models.DbModels;
using Museum.Models.Interfaces.Service;
using Museum.Models.Interfaces.Repository;

namespace Museum.Services
{
    public class TicketService : BaseService<Ticket, Ticket, Ticket, Ticket>, ITicketService
    {
        public TicketService(ITicketRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }
    }
}
