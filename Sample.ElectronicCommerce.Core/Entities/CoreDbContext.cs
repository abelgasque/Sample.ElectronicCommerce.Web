using Microsoft.EntityFrameworkCore;
using Sample.ElectronicCommerce.Core.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Helpers;
using System;
using System.Linq;
using System.Reflection;

namespace Sample.ElectronicCommerce.Core.Entities
{
    public class CoreDbContext : DbContext
    {
        private readonly DataBaseHelper _dbHelper;

        public DbSet<ApplicationEntity> Application { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options, DataBaseHelper dbHelper) : base(options)
        {
            _dbHelper = dbHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbHelper.CoreConnectionStringSql());
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
