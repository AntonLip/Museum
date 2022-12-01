using Museum.Services;
using Museum.DataAccess;
using Museum.Models.DbModels;
using Museum.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Museum.Models.Interfaces.Service;
using Museum.Models.Interfaces.Repository;

namespace Museum
{
    public static class IServiceCollectionExtensions
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IExhibitCategoryRepository, ExhibitCategoryRepository>();
            services.AddTransient<IExhibitRepository, ExhibitRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IExhibitCategoryService, ExhibitCategoryService>();
            services.AddTransient<IExhibitService, ExhibitService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IFileService, FileService>();
        }
    }
}
