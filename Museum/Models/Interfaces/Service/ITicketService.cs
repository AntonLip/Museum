using Museum.Models.DbModels;
using Museum.Models.DtoModels;

namespace Museum.Models.Interfaces.Service
{
    public interface ITicketService : IService<Ticket, Ticket, Ticket, Ticket, Guid>
    {
        List<Ticket> GetByUserId(string userId);
        Task<List<StatisticDto>> GetStatisticDataCurrentYear();
    }
}
