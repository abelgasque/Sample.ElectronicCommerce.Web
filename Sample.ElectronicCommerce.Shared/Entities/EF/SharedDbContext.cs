using Microsoft.EntityFrameworkCore;
using Sample.ElectronicCommerce.Shared.Entities.Mapping;
using Sample.ElectronicCommerce.Shared.Helpers;
using System;
using System.Linq;
using System.Reflection;

namespace Sample.ElectronicCommerce.Shared.Entities.EF
{
    public class SharedDbContext : DbContext
    {
        private readonly DataBaseHelper _dbHelper;

        public SharedDbContext(
            DbContextOptions<SharedDbContext> options, 
            DataBaseHelper dbHelper
        ) : base(options) {
            _dbHelper = dbHelper;
        }

        public DbSet<LogAppEntity> LogApp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbHelper.SharedConnectionStringSql());
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
