using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleApp.Repository
{
    [Table("Users")]
    [Index("UserName", IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }

    [Table("Roles")]
    [Index("RoleName", IsUnique = true)]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [MinLength(5)]
        public string RoleName { get; set; } 
        public string RoleDescription { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }

    [Table("UserRoles")]
    [PrimaryKey("UserId", "RoleId")]
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }

    public class UserRoleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"server=(local)\MSSQLSERVER2;database=UsersDB;integrated security=sspi;trustservercertificate=true"
                );
        }
    }
}
