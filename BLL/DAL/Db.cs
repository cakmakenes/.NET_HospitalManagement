using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }


        public Db(DbContextOptions options) : base(options)
        {
        }
    }
}
