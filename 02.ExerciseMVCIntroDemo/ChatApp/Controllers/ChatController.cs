namespace ChatApp.Controllers
{
    using ChatApp.Models.Message;
    using Microsoft.AspNetCore.Mvc;

    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> messeges =
            new List<KeyValuePair<string, string>>();

        public IActionResult Show()
        {
            if (messeges.Count() < 1)
            {
                return View(new ChatViewModel());
            }

            var chatModel = new ChatViewModel()
            {
                Messages = messeges
                           .Select(m => new MessageViewModel()
                           {
                               Sender = m.Key,
                               MessageText = m.Value
                           })
                           .ToList()
            };

            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMessage = chat.CurrentMessage;
            messeges.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));

            return RedirectToAction("Show");
        }
    }
}
