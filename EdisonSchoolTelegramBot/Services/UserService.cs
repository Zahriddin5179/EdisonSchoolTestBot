using EdisonSchoolTelegramBot.Abstractions;
using EdisonSchoolTelegramBot.Interfaces;
using EdisonSchoolTelegramBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace EdisonSchoolTelegramBot.Services
{
    public class UserService : IUserService
    {
        private readonly BotDbContext _dbContext;
        private readonly ITelegramBotClient _bot;
        public UserService(BotDbContext botDbContext, ITelegramBotClient bot)
        {
            _dbContext = botDbContext;
            _bot = bot;
        }

        public async Task<TelegramUser> EnsureUserExists(long chatId,string? username = null,string? fullName = null)
        {
            var user = await _dbContext.TelegramUsers.FirstOrDefaultAsync(x => x.ChatId == chatId);

            if (user == null)
            {
                user = new TelegramUser
                {
                    ChatId = chatId,
                    Username = username,
                    FullName = fullName,
                    CreatedAt = DateTime.UtcNow
                };

                _dbContext.TelegramUsers.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<string> GetState(long chatId)
        {
            var state = await _dbContext.TelegramUserStates.FirstOrDefaultAsync(x => x.ChatId == chatId);
            return state?.State ?? "START";
        }

        public async Task SetState(long chatId, string state)
        {
            var userState = await _dbContext.TelegramUserStates
                .FirstOrDefaultAsync(x => x.ChatId == chatId);

            if (userState == null)
            {
                userState = new TelegramUserState
                {
                    ChatId = chatId,
                    State = state,
                    UpdatedAt = DateTime.UtcNow
                };
                _dbContext.TelegramUserStates.Add(userState);
            }
            else
            {
                userState.State = state;
                userState.UpdatedAt = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task SaveFullName(long chatId, string fullName)
        {
            var user = await _dbContext.TelegramUsers
                .FirstAsync(x => x.ChatId == chatId);

            user.FullName = fullName;
            await _dbContext.SaveChangesAsync();

            await SetState(chatId, "WAITING_PHONE");

            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("📱 Telefon raqamni yuborish")
                    {
                        RequestContact = true
                    }
                }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
            await _bot.SendMessage(
                chatId: chatId,
                text: "📱 Iltimos, telefon raqamingizni yuboring:",
                replyMarkup: keyboard
            );

        }

        public async Task<bool> SavePhone(long chatId, string phone)
        {
            if (!Regex.IsMatch(phone, @"^\+?\d{9,15}$"))
                return false;

            var user = await _dbContext.TelegramUsers
                .FirstAsync(x => x.ChatId == chatId);

            user.PhoneNumber = phone;
            await _dbContext.SaveChangesAsync();

            await SetState(chatId, "WAITING_AGE");

            InlineKeyboardMarkup ageKeyboard = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("12", "12"),
                    InlineKeyboardButton.WithCallbackData("13", "13"),
                    InlineKeyboardButton.WithCallbackData("14", "14"),
                    InlineKeyboardButton.WithCallbackData("15", "15"),
                    InlineKeyboardButton.WithCallbackData("16", "16")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("17", "17"),
                    InlineKeyboardButton.WithCallbackData("18", "18"),
                    InlineKeyboardButton.WithCallbackData("19", "19"),
                    InlineKeyboardButton.WithCallbackData("20", "20"),
                    InlineKeyboardButton.WithCallbackData("21", "21"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("22", "22"),
                    InlineKeyboardButton.WithCallbackData("23", "23"),
                    InlineKeyboardButton.WithCallbackData("24", "24"),
                    InlineKeyboardButton.WithCallbackData("25", "25"),
                    InlineKeyboardButton.WithCallbackData("26", "26"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("27", "27"),
                    InlineKeyboardButton.WithCallbackData("28", "28"),
                    InlineKeyboardButton.WithCallbackData("29", "29"),
                    InlineKeyboardButton.WithCallbackData("30", "30"),
                    InlineKeyboardButton.WithCallbackData("30+", "31"),
                }
            });

            await _bot.SendMessage(chatId,"🎂 Yoshingizni tanlang:",replyMarkup: ageKeyboard);
            return true;
        }

        public async Task<bool> SaveAge(long chatId, string ageText)
        {
            if (!int.TryParse(ageText, out int age) || age < 5 || age > 100)
                return false;

            var user = await _dbContext.TelegramUsers
                .FirstAsync(x => x.ChatId == chatId);

            user.Age = age;
            await _dbContext.SaveChangesAsync();

            await SetState(chatId, "READY_FOR_TEST");
            await _bot.SendMessage(chatId, "✅ Ro‘yxatdan o‘tdingiz. Testlar yuklanmoqda...");
            return true;
        }

        public async Task SendTestList(long chatId)
        {
            var tests = await _dbContext.Tests
                .Where(x => x.IsActive)
                .ToListAsync();

            if (!tests.Any())
            {
                await _bot.SendMessage(chatId, "❌ Testlar mavjud emas.");
                return;
            }

            var buttons = tests.Select(t =>
                InlineKeyboardButton.WithCallbackData(
                    t.Title,
                    $"test_{t.Id}"
                )
            ).Chunk(1);

            await _bot.SendMessage(
                chatId,
                "📝 Testni tanlang:",
                replyMarkup: new InlineKeyboardMarkup(buttons)
            );
        }

    }
}
