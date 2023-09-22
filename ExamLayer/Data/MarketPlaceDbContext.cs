using ExamLayer.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExamLayer.Data
{
    public class MarketPlaceDbContext : DbContext
    {
        public MarketPlaceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AZDPS_APIM_INDEX_LIST> AZDPS_APIM_INDEX_LISTs { get; set; }
        public DbSet<AZDPS_APIM_INDEX_LIST> IndexBusinessMeta { get; set; }
    }
}
