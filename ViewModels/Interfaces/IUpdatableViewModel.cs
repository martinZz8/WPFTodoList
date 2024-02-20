using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DTO;

namespace TodoList.ViewModels.Interfaces
{
    internal interface IUpdatableViewModel<T>
    {
        void UpdateCollection(IEnumerable<T> collectionFrom);
    }
}
