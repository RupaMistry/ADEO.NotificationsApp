using ADEO.NotificationsApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ADEO.NotificationsApp.DAL.Core
{ 
    /// <summary>
    /// IRepository for MeetingDetails entity
    /// </summary>
    public class UserMessageRepository(NotificationAppDBContext dbContext, IMemoryCache memoryCache) : IUserMessageRepository<UserMessage>
    {  
        private readonly NotificationAppDBContext _appDbContext = dbContext;

        /// <summary>
        /// Returns list of user messages for given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List of Messages</returns>
        public async Task<IReadOnlyList<UserMessage>> GetAllAsync(DateTime date)
        {
            try
            {
                var messages = await this._appDbContext.UserMessages
                .Where(m => m.MessageDate.Date >= date.Date && m.EndPublishedDate == null)
                .OrderBy(n => n.MessageDate)
                .ToListAsync();

                return messages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns list of published user messages.
        /// </summary> 
        /// <returns>List of Messages</returns>
        public async Task<IReadOnlyList<UserMessage>> GetHistoryAsync()
        {
            try
            {
                var messages = await this._appDbContext.UserMessages
                .Where(m => m.IsPublished && m.EndPublishedDate != null)
                .OrderBy(n => n.EndPublishedDate)
                .ToListAsync();

                return messages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns message details by ID.
        /// </summary>
        /// <param name="messageID"></param> 
        /// <returns>UserMessage</returns>
        public async Task<UserMessage> GetAsync(int messageID)
        {
            try
            {
                // Query and return all messages that are still not delivered to recipient by sender
                var message = await this._appDbContext.UserMessages
                     .Where(m => m.ID == messageID).FirstOrDefaultAsync(); 

                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts a new call history record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Rows affected count</returns>
        public async Task<int> InsertUserMessage(UserMessage userMessage)
        {
            var messageDate = new DateTime(userMessage.MessageDate.Year, userMessage.MessageDate.Month,
                userMessage.MessageDate.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            try
            {
                var message = new UserMessage()
                {
                    Content = userMessage.Content,
                    MessageDate = messageDate,
                    IsPublished = false
                };

                this._appDbContext.UserMessages.Add(message);

                int rowsAffected = await this._appDbContext.SaveChangesAsync();

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Publishes or send a message to screen.
        /// </summary>
        /// <param name="messageID"></param> 
        /// <returns>Rows affected count</returns>
        public async Task<int> PublishMessage(int messageID)
        {
            try
            {
                var oldPublishedMessageID = Convert.ToInt32(memoryCache.Get("OldPublishedMessageID"));
                 
                if (oldPublishedMessageID > 0)
                    await this.EndPublishMessage(oldPublishedMessageID);

                var message = await this.GetAsync(messageID);
                message.IsPublished = true;
                message.PublishedDate = DateTime.Now;

                // TO Clear on how to pick previous message record & do end publish

                int rowsAffected = await this._appDbContext.SaveChangesAsync();

                memoryCache.Set("OldPublishedMessageID", messageID);

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Ends the publish of message
        /// </summary>
        /// <param name="messageID"></param> 
        /// <returns>Rows affected count</returns>
        public async Task<int> EndPublishMessage(int messageID)
        {
            try
            {
                var message = await this.GetAsync(messageID);

                // If message was already end published through button, return.
                if (message.EndPublishedDate != null)
                    return -1;

                message.IsPublished = true;
                message.EndPublishedDate = DateTime.Now; 

                int rowsAffected = await this._appDbContext.SaveChangesAsync();

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Edits user message.
        /// </summary>
        /// <param name="userMessage">The user message.</param>
        /// <returns><![CDATA[Task<int>]]></returns>
        public async Task<int> EditUserMessage(UserMessage userMessage)
        {
            var messageDate = new DateTime(userMessage.MessageDate.Year, userMessage.MessageDate.Month,
               userMessage.MessageDate.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
           
            try
            {
                var message = await this.GetAsync(userMessage.ID);

                message.Content = userMessage.Content;
                message.MessageDate = messageDate;   

                int rowsAffected = await this._appDbContext.SaveChangesAsync();

                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}