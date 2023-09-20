using ExamLayer.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExamLayer.Data
{
    public class MarketPlaceDbContext : DbContext
    {
        public MarketPlaceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<AZDPS_APIM_INDEX_LIST> IndexBusinessMeta { get; set; }
    }
}
