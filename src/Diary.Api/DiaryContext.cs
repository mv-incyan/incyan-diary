using Microsoft.EntityFrameworkCore;

namespace Diary.Api
{
    public class DiaryContext : DbContext
    {
        public DiaryContext(DbContextOptions<DiaryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Diary> Diaries { get; set; }
    }
}