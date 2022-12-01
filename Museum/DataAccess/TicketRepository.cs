using Museum.Models.DbModels;
using Museum.Models.Interfaces.Repository;

namespace Museum.DataAccess
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(AppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
