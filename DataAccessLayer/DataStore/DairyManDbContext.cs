using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.EntityModels;

namespace DataAccessLayer.DataStore
{
    public class DairyManDbContext : IdentityDbContext<DairyMan>
    {
        public DairyManDbContext(DbContextOptions<DairyManDbContext> options)
             : base(options)
        { }


    }
}
