using EdisonSchoolTelegramBot.Models;

namespace EdisonSchoolTelegramBot.Interfaces
{
    public interface IUserService
    {
        Task<TelegramUser> EnsureUserExists(long chatId, string? username = null, string? fullName = null);

        // State bilan ishlash
        Task<string> GetState(long chatId);
        Task SetState(long chatId, string state);

        // User ma’lumotlarini saqlash
        Task SaveFullName(long chatId, string fullName);
        Task<bool> SavePhone(long chatId, string phone);
        Task<bool> SaveAge(long chatId, string ageText);

        Task SendTestList(long chatId);
    }
}
