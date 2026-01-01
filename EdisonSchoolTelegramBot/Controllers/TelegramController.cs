using EdisonSchoolTelegramBot.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace EdisonSchoolTelegramBot.Controllers
{
    [ApiController]
    [Route("api/telegram")]
    public class TelegramController : ControllerBase
    {
        private readonly ITelegramBotClient _bot;
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IButtonService _buttonService;

        public TelegramController(ITelegramBotClient bot, ILogger<TelegramController> logger, IUserService userService, IButtonService buttonService)
        {
            _bot = bot;
            _logger = logger;
            _userService = userService;
            _buttonService = buttonService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            //ChatId ni aniqlab olamiz
            long chatId = 0;
            string msgText = string.Empty;
            try
            {
                if (update.Message != null)
                {
                    chatId = update.Message.Chat.Id;
                    if (!string.IsNullOrEmpty(update.Message.Text))
                    {
                        msgText = update.Message.Text.Trim();
                        if (msgText == "/start")
                        {
                            var user = await _userService.EnsureUserExists(
                                chatId,
                                update.Message.Chat.Username
                            );

                            string userState = await _userService.GetState(user.ChatId);
                            if (!string.IsNullOrEmpty(userState))
                            {
                                await _buttonService.GetMenuByState(chatId,userState);
                            }
                            else
                            {
                                await _userService.SetState(chatId, "WAITING_FULLNAME");

                                await _bot.SendMessage(
                                    chatId,
                                    "👋 Assalomu alaykum!\n\nIltimos, FIO kiriting:"
                                );
                            }
                            return Ok();
                        }
                    }
                }
                else if (update.CallbackQuery != null)
                {
                    chatId = update.CallbackQuery.Message.Chat.Id;
                }

                var state = await _userService.GetState(chatId);

                switch (state)
                {
                    case "WAITING_FULLNAME":
                        await _userService.SaveFullName(chatId, msgText);
                        await _buttonService.GetMenuByState(chatId, "WAITING_FULLNAME");
                        break;

                    case "WAITING_PHONE":
                        if (update.Message?.Contact != null)
                        {
                            var phone = update.Message.Contact.PhoneNumber;
                            await _userService.SavePhone(chatId, phone);
                            await _buttonService.GetMenuByState(chatId, "WAITING_PHONE");
                        }
                        break;

                    case "WAITING_AGE":
                        if (update.CallbackQuery != null)
                        {
                            var data = update.CallbackQuery.Data;
                            await _userService.SaveAge(chatId, data);
                            await _userService.SendTestList(chatId);
                            await _buttonService.GetMenuByState(chatId, "WAITING_AGE");
                        }
                        break;

                    case "READY_FOR_TEST":
                        if (update.CallbackQuery != null)
                        {
                            await _buttonService.GetMenuByState(chatId, "READY_FOR_TEST");
                        }
                        break;

                    case "IN_TEST":
                        await _buttonService.GetMenuByState(chatId, "IN_TEST");
                        break;
                    default:
                        await _bot.SendMessage(chatId, "❌ Iltimos, /start buyrug‘i orqali boshlang.");
                        break;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Xatolik yuz berdi: {Message}", ex.Message);
            }

            return Ok();
        }
    }
}
