using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.DTO
{
    internal class TodoItemAddDTO
    {
        public string Name { get; set; }
        public int StatusId { get; set; }

        public TodoItemAddDTO()
        {
        }

        public TodoItemAddDTO(string name, int statusId)
        {
            Name = name;
            StatusId = statusId;
        }
    }
}
