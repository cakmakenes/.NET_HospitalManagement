using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
        }
    }
}
