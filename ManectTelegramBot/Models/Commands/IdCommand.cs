using ManectTelegramBot.Abstractions;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace ManectTelegramBot.Models.Commands
{
    public class IdCommand: TelegramCommand
    {
        public override string Name => @"/id";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, chatId.ToString(), parseMode: ParseMode.Markdown);
        }
    }
}