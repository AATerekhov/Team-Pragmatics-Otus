using Microsoft.EntityFrameworkCore;
using UserApi.DataAccess.Entities;

namespace UserApi.DataAccess.EntityFramework
{
    public class DataContext(DbContextOptions<DataContext> options):DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
