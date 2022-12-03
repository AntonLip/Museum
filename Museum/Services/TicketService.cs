using AutoMapper;
using Museum.Models.DbModels;
using Museum.Models.Interfaces.Service;
using Museum.Models.Interfaces.Repository;
using Museum.Models.DtoModels;
using System.Globalization;

namespace Museum.Services
{
    public class TicketService : BaseService<Ticket, Ticket, Ticket, Ticket>, ITicketService
    {
        public TicketService(ITicketRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }

        public  List<Ticket> GetByUserId(string userId)
        {
            var tikets =  _repository.GetWithInclude(p => p.UserId == userId);
            return tikets;
        }

        public async Task<List<StatisticDto>> GetStatisticDataCurrentYear()
        {
            var tickets = await _repository.GetAllAsync();
            if (tickets is null || tickets.Count() == 0)
                return new List<StatisticDto>();

            var model = new List<StatisticDto>();
            var t = tickets.Where(p => p.VisitTime.Year == DateTime.Now.Year).ToList();
            for (int i = 1; i < 13; i++)
            {
                int count = t.Where(p => p.VisitTime.Month == i).Count();
                
                string str = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i); 
                model.Add(new StatisticDto 
                {
                    DimensionOne = str,
                    Quantity = count
                });
            }
            return model;
        }
    }
}
