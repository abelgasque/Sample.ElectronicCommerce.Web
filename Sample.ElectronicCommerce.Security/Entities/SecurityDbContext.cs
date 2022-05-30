using Microsoft.EntityFrameworkCore;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Helpers;
using System;
using System.Linq;
using System.Reflection;

namespace Sample.ElectronicCommerce.Security.Entities
{
    public class SecurityDbContext : DbContext
    {
        private readonly DataBaseHelper _dbHelper;

        public DbSet<UserEntity> User { get; set; }

        public DbSet<UserRoleEntity> UserRole { get; set; }

        public DbSet<UserHasRoleEntity> UserHasRole { get; set; }

        public DbSet<UserSessionEntity> UserSession { get; set; }

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options, DataBaseHelper dbHelper) : base(options)
        {
            _dbHelper = dbHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbHelper.SecurityConnectionStringSql());
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
