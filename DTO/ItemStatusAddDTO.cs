using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.DTO
{
    internal class ItemStatusAddDTO
    {
        public string Name { get; set; }

        public ItemStatusAddDTO()
        {
        }

        public ItemStatusAddDTO(string Name)
        {
            this.Name = Name;
        }
    }
}
