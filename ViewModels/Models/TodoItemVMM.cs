using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TodoList.DTO;

namespace TodoList.ViewModels.Models
{
    internal class TodoItemVMM: ViewModelBase
    {
        private readonly TodoItemDTO _todoItemDto;

        public int Id => _todoItemDto.Id;

        public string Name => _todoItemDto.Name;

        public int StatusId => _todoItemDto.Status.Id;

        public string StatusName => _todoItemDto.Status.Name;

        public string CreationDate => _todoItemDto.CreationDate.ToString("dd/MM/yyyy");

        public TodoItemVMM(TodoItemDTO todoItemDto)
        {
            _todoItemDto = todoItemDto;
        }
    }
}
