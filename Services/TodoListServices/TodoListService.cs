using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DbContexts;
using TodoList.DTO;
using TodoList.Models;

namespace TodoList.Services.TodoListServices
{
    internal class TodoListService : ITodoListService
    {
        private readonly TodoListDbContext _dbContext;
        
        public TodoListService(string connectionString)
        {
            _dbContext = new TodoListDbContextFactory(connectionString).CreateDbContext();
        }

        // Note! - Here "Include" is required, since data is loaded lazy in EF (otherwise we wouldn't have "Status" prop filled)
        public async Task<IEnumerable<TodoItemDTO>> GetAllTodoItems()
        {
            IEnumerable<TodoItem> todoItems = await _dbContext.TodoItem
                .Include(it => it.Status)
                .ToListAsync();
            
            return todoItems
                .Select(it => new TodoItemDTO(it))
                .OrderByDescending(it => it.CreationDate);

            // Other way:
            // List<TodoItemDTO> todoItemsDTO = todoItems.Select(it => new TodoItemDTO(it)).ToList();
            // todoItemsDTO.Sort((it1, it2) => it2.CreationDate.CompareTo(it1.CreationDate));
            // return todoItemsDTO;
        }

        // Note! - Here "Include" is required, since data is loaded lazy in EF (otherwise we wouldn't have "Status" prop filled)
        public async Task<TodoItemDTO> GetTodoItem(int id)
        {
            TodoItem todoItem = await _dbContext.TodoItem
                .Where(it => it.Id == id)
                .Include(it => it.Status)
                .FirstOrDefaultAsync();

            if (todoItem != null)
            {
                TodoItemDTO todoItemToRet = new TodoItemDTO(todoItem);

                return todoItemToRet;
            }

            return null;
        }

        public async Task<bool> CreateTodoItem(TodoItemAddDTO todoItemDto)
        {
            ItemStatus foundItemStatus = await _dbContext.ItemStatus.FirstOrDefaultAsync(it => it.Id == todoItemDto.StatusId);

            if (foundItemStatus != null && todoItemDto.Name.Length > 0)
            {
                // Don't have to set property "CreationDate = DateTime.UtcNow", because DB handles it (by setting "TodoListDbContext.OnModelCreating" method)
                TodoItem todoItemToAdd = new TodoItem()
                {
                    Name = todoItemDto.Name,
                    Status = foundItemStatus
                };

                await _dbContext.TodoItem.AddAsync(todoItemToAdd);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<TodoItemDTO> UpdateTodoItem(TodoItemAddDTO todoItemDto, int id)
        {
            ItemStatus foundItemStatus = await _dbContext.ItemStatus.FirstOrDefaultAsync(it => it.Id == todoItemDto.StatusId);

            TodoItem todoItem = await _dbContext.TodoItem
                .Where(it => it.Id == id)
                .Include(it => it.Status)
                .FirstOrDefaultAsync();

            if (todoItem != null && foundItemStatus != null)
            {
                // Here we could only change 'Status' or 'ItemStatusID' properties to successfully update todoItem in DB.
                // But we want to return updated object from this method, so we update both properties (to stay consistent).
                todoItem.Status = foundItemStatus;
                todoItem.ItemStatusID = foundItemStatus.Id;
                todoItem.Name = todoItemDto.Name;

                await _dbContext.SaveChangesAsync();

                TodoItemDTO todoItemToRet = new TodoItemDTO(todoItem);

                return todoItemToRet;
            }

            return null;
        }

        public async Task<bool> DeleteTodoItem(int id)
        {
            TodoItem todoItem = await _dbContext.TodoItem.FirstOrDefaultAsync(it => it.Id == id);

            if (todoItem != null)
            {
                _dbContext.Remove(todoItem);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
