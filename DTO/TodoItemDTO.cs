using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.DTO
{
    internal class TodoItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemStatusDTO Status { get; set; }
        public DateTime CreationDate { get; set; }

        public TodoItemDTO()
        {
        }

        public TodoItemDTO(TodoItem todoItem)
        {
            this.Id = todoItem.Id;
            this.Name = todoItem.Name;
            this.Status = new ItemStatusDTO(todoItem.Status);
            this.CreationDate = todoItem.CreationDate;
        }

        public override string ToString()
        {
            return $"id: {Id}\nname: {Name}\nstatus id: {Status.Id}\nstatus name: {Status.Name}\ncreation date: {CreationDate.ToString("dd/mm/yyyy")}";
        }
    }
}
