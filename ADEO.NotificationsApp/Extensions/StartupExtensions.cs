using ADEO.NotificationsApp.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADEO.NotificationsApp.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void SetupDatabase(this IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<NotificationAppDBContext>(options =>
                        options.UseSqlServer(dbConnection,
                        o => o.MigrationsHistoryTable(tableName: HistoryRepository.DefaultTableName)));
        }
    }
}