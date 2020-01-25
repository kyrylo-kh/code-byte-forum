using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodeByteForum.Models;
using CodeByteForum.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeByteForum.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Добавление Tags в модель = List<string> переходит в string, разделенный ';'.
            // Вернуть из модели = string в List<string>.
            var splitStringConverter = new ValueConverter<List<string>, string>(
                v => string.Join(";", v), 
                v => v.Split(new[] { ';' }).ToList());

            builder
                .Entity<Post>()
                .Property(p => p.Tags)
                .HasConversion(splitStringConverter);
        }
    }
}