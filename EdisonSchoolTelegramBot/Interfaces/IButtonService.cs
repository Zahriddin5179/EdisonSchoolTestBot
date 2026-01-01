using Telegram.Bot.Types.ReplyMarkups;

namespace EdisonSchoolTelegramBot.Interfaces
{
    public interface IButtonService
    {
        Task GetMenuByState(long chatId,string state);
    }


}
