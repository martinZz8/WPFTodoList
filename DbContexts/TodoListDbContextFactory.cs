using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.DbContexts
{
    internal class TodoListDbContextFactory
    {
        private readonly string _connectionString;

        public TodoListDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TodoListDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlServer(_connectionString)
                .Options;

            return new TodoListDbContext(options);
        }
    }
}
