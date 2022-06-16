using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities.Mapping;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using System;
using System.Linq;
using System.Reflection;

namespace Sample.ElectronicCommerce.Core.Entities.EF
{
    public class SharedDbContext : DbContext
    {
        private readonly SharedSettings _sharedSettings;

        public SharedDbContext(
            DbContextOptions<SharedDbContext> options,
            IOptions<SharedSettings> sharedSettings

        ) : base(options) {
            _sharedSettings = sharedSettings.Value;
        }

        public DbSet<LogAppEntity> LogApp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sharedSettings.GetConnectionString);
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
