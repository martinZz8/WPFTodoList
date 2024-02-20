using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DTO;

namespace TodoList.Services.TodoListServices
{
    internal interface ITodoListService
    {
        Task<IEnumerable<TodoItemDTO>> GetAllTodoItems();
        Task<TodoItemDTO> GetTodoItem(int id);

        Task<bool> CreateTodoItem(TodoItemAddDTO todoItemDto);

        Task<TodoItemDTO> UpdateTodoItem(TodoItemAddDTO todoItemDto, int id);

        Task<bool> DeleteTodoItem(int id);
    }
}
