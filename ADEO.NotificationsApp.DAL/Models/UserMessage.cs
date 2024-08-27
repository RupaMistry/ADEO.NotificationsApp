using System.ComponentModel.DataAnnotations;

namespace ADEO.NotificationsApp.DAL.Models
{
    public class UserMessage : Entity
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime MessageDate { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedDate { get; set; }

        public DateTime? EndPublishedDate { get; set; } 
    }
}