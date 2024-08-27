using ADEO.NotificationsApp.DAL.Models;

namespace ADEO.NotificationsApp.DAL.Core
{
    /// <summary>
    /// IRepository for UserMessage entity
    /// </summary>
    public interface IUserMessageRepository<T> where T : Entity
    {
        /// <summary>
        /// Returns list of UserMessages for given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List of UserMessages</returns>
        Task<IReadOnlyList<T>> GetAllAsync(DateTime date);
       
        /// <summary>
        /// Returns list of published user messages.
        /// </summary> 
        /// <returns>List of UserMessages</returns>
        Task<IReadOnlyList<T>> GetHistoryAsync();
       
        /// <summary>
        /// Returns UserMessage details by ID.
        /// </summary>
        /// <param name="messageID"></param> 
        /// <returns>UserMessage</returns>
        Task<T> GetAsync(int messageID);

        Task<int> InsertUserMessage(UserMessage userMessage);

        Task<int> EditUserMessage(UserMessage userMessage);

        Task<int> PublishMessage(int messageID);

        Task<int> EndPublishMessage(int messageID);
    }
}