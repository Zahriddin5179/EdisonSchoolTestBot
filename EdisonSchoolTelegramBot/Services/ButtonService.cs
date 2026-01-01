using EdisonSchoolTelegramBot.Abstractions;
using EdisonSchoolTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EdisonSchoolTelegramBot.Services
{
    public class ButtonService : IButtonService
    {
        private readonly BotDbContext _dbContext;
        private readonly ITelegramBotClient _bot;

        public ButtonService(BotDbContext botDbContext, ITelegramBotClient bot)
        {
            _dbContext = botDbContext;
            _bot = bot;
        }


        public async Task GetMenuByState(long chatId, string state)
        {
            if (state == "READY_FOR_TEST")
                await GetMainMenu(chatId);
        }

        public async Task GetMainMenu(long chatId)
        {
            ReplyKeyboardMarkup replyMarkup =  new ReplyKeyboardMarkup(new[]
            {
            new KeyboardButton[] { "📝 Testlar", "🧠 Diagnostika" },
            new KeyboardButton[] { "👤 Kabinet", "🆘 Support" }
            })
                {
                    ResizeKeyboard = true
                };

            await _bot.SendMessage(chatId,"Kerakli bo‘limni tanlang 👇",replyMarkup: replyMarkup);
        }
    }

}
