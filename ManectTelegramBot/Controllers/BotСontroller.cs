using System.Threading.Tasks;
using Telegram.Bot.Types;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using ManectTelegramBot.Abstractions;

namespace ManectTelegramBot.Controllers
{
    [ApiController]
    [Route(@"api/bot")]
    public class BotСontroller: Controller
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICommandService _commandService;
        public BotСontroller(ICommandService commandService, ITelegramBotClient telegramBotClient)
        {
            _commandService = commandService;
            _telegramBotClient = telegramBotClient;
        }

        [HttpGet]
        public string Index()
        {
            return "Started";
        }

        [HttpPost]
        public async Task<OkResult> Post([FromBody] Update update)
        {
            if (update == null) return Ok();

            var message = update.Message;

            foreach (var command in _commandService.GetCommands())
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, _telegramBotClient);
                    return Ok();
                }
            }

            await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "Твою писанину даже я непонял...\n" + "Твоя писанина: " + update.Message.Text);

            return Ok();
        }
    }
}
