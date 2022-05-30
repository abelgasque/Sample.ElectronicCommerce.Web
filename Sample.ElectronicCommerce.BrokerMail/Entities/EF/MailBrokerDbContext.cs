using Microsoft.EntityFrameworkCore;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Helpers;
using System;
using System.Linq;
using System.Reflection;

namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF
{
    public class MailBrokerDbContext : DbContext
    {
        private readonly DataBaseHelper _dbHelper;

        public DbSet<MailBrokerEntity> MailBroker { get; set; }

        public DbSet<MailEntity> Mail { get; set; }

        public DbSet<MailHasBrokerEntity> MailHasBroker { get; set; }

        public DbSet<MailMessageEntity> MailMessage { get; set; }

        public MailBrokerDbContext(
            DbContextOptions<MailBrokerDbContext> options, 
            DataBaseHelper dbHelper
        ) : base(options) 
        {
            _dbHelper = dbHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbHelper.BrokerMailConnectionStringSql());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister =
                    Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType
                    && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
