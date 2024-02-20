using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.DbContexts
{
    internal class TodoListDbContext: DbContext
    {
        public TodoListDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<ItemStatus> ItemStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("getutcdate()"); //getdate()

            base.OnModelCreating(modelBuilder);
        }
    }
}
