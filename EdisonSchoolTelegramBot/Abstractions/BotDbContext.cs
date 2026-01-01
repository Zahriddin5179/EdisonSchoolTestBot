using EdisonSchoolTelegramBot.Models;
using Microsoft.EntityFrameworkCore;

namespace EdisonSchoolTelegramBot.Abstractions
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options): base(options) { }

        public DbSet<Test> Tests => Set<Test>();
        public DbSet<TestQuestion> TestQuestions => Set<TestQuestion>();
        public DbSet<TestOption> TestOptions => Set<TestOption>();
        public DbSet<TestAttempt> TestAttempts => Set<TestAttempt>();
        public DbSet<TestAttemptAnswer> TestAttemptAnswers => Set<TestAttemptAnswer>();
        public DbSet<TelegramUser> TelegramUsers => Set<TelegramUser>();
        public DbSet<TelegramUserState> TelegramUserStates => Set<TelegramUserState>();
        public DbSet<Subject> Subjects => Set<Subject>();

    }

}
