using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContexts
{
    public class ApartmentSystemDbContext:DbContext
    {
        private IConfiguration _configuration;

        public ApartmentSystemDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Defining tables for SQL database
        public DbSet<House> Houses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string definition

            var connectionString = _configuration.GetConnectionString("MsComm");
            base.OnConfiguring(optionsBuilder.UseSqlServer(connectionString));
        }
    }
}
