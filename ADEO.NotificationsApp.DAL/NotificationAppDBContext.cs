using ADEO.NotificationsApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ADEO.NotificationsApp.DAL
{
    /// <summary>
    /// The notification app DB context.
    /// </summary>
    /// <param name="options">The options.</param>
    public class NotificationAppDBContext(DbContextOptions<NotificationAppDBContext> options) : DbContext(options)
    { 
        public DbSet<UserMessage> UserMessages { get; set; }   
         
        /// <summary>
        /// Configures the schema needed for the ChatApp db.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
        } 
    }
}