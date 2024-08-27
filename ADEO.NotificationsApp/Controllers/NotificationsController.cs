using ADEO.NotificationsApp.DAL.Core;
using ADEO.NotificationsApp.DAL.Models;
using ADEO.NotificationsApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ADEO.NotificationsApp.Web.Controllers
{
    /// <summary>
    /// The notifications controller.
    /// </summary>
    /// <param name="meetingRepository">The meeting repository.</param>
    /// <param name="logger">The logger.</param>
    public class NotificationsController(
        IUserMessageRepository<UserMessage> messageRepository,
        ILogger<NotificationsController> logger) : Controller
    {
        private readonly IUserMessageRepository<UserMessage> _messageRepository = messageRepository;
        private readonly ILogger<NotificationsController> _logger = logger;

        public async Task<IActionResult> Index()
        {
            NotificationsModel notificationsModel = await GetNotificationsDataList();

            return View(notificationsModel);
        }

        private async Task<NotificationsModel> GetNotificationsDataList()
        {
            return new NotificationsModel
            {
                UserMessages = await this._messageRepository.GetAllAsync(DateTime.Now),

                MessageHistory = await this._messageRepository.GetHistoryAsync()
            };
        }

        /// <summary>
        /// Get notification tabs partial.
        /// </summary>
        /// <returns><![CDATA[Task<PartialViewResult>]]></returns>
        public async Task<PartialViewResult> GetNotificationTabsPartial()
        {
            NotificationsModel notificationsModel = await GetNotificationsDataList();

            return PartialView("_NotificationTabsPartial", notificationsModel);
        }

        public IActionResult CreateNewMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewMessage(UserMessage userMessage)
        {
            if (!ModelState.IsValid)
                return View();

            var rowsAffected = await this._messageRepository.InsertUserMessage(userMessage);  
            return Json(rowsAffected); 
        }


        [HttpPost]
        public async Task<IActionResult> EditMessage(UserMessage userMessage)
        {
            if (!ModelState.IsValid)
                return View();

            var rowsAffected = await this._messageRepository.EditUserMessage(userMessage);

            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> PublishMessage(int messageID)
        {
            if (!ModelState.IsValid)
                return View();

            var rowsAffected = await this._messageRepository.PublishMessage(messageID);

            return Json(rowsAffected);
        }

        [HttpPost]
        public async Task<IActionResult> EndPublishMessage(int messageID)
        {
            if (!ModelState.IsValid)
                return View();

            var rowsAffected = await this._messageRepository.EndPublishMessage(messageID);

            return Json(rowsAffected);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}