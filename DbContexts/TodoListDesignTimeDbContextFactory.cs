using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Static;

namespace TodoList.DbContexts
{
    internal class TodoListDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TodoListDbContext>
    {
        // From: https://learn.microsoft.com/pl-pl/ef/core/miscellaneous/connection-strings (only added 'Integrated Security=SSPI' as default Windows authentication method)
        // Can also specify:
        // 'User Id='
        // 'Password='
        private const string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=todoListProject;Integrated Security=SSPI";

        public TodoListDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlServer(_connectionString)
                .Options;

            return new TodoListDbContext(options);
        }
    }
}
