using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModels.Models;

namespace TodoList.Wrappers
{
    internal class SelectTodoItem
    {
        private TodoItemVMM _item = null;

        public TodoItemVMM Item
        {
            get
            {
                return _item;
            }

            set
            {
                _item = value;
            }
        }

        public SelectTodoItem()
        {
        }
    }
}
