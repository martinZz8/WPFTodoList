using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.DTO
{
    internal class ItemStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ItemStatusDTO()
        {
        }

        public ItemStatusDTO(ItemStatus itemStatus)
        {
            this.Id = itemStatus.Id;
            this.Name = itemStatus.Name;
        }

        public override string ToString()
        {
            return $"id: {Id}\nname: {Name}";
        }
    }
}
