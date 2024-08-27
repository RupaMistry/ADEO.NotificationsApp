using ADEO.NotificationsApp.DAL.Models;

namespace ADEO.NotificationsApp.Web.Models
{
    public class NotificationsModel
    {
        public IReadOnlyList<UserMessage> UserMessages { get; set; }

        public IReadOnlyList<UserMessage> MessageHistory { get; set; }
    }

    public class MessageCreationResponse
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; } 

        public NotificationsModel NotificationsModel { get; set; }
    }
}