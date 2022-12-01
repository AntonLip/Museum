using Museum.Models.DbModels;

namespace Museum.Models.Interfaces.Repository
{
    public interface ITicketRepository : IRepository<Ticket, Guid>
    {
    }
}
