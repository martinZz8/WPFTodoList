using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DTO;

namespace TodoList.Services.ItemStatusServices
{
    internal interface IItemStatusService
    {
        Task<IEnumerable<ItemStatusDTO>> GetAllItemStatuses();

        Task<ItemStatusDTO> GetItemStatusByName(string name);
        Task<ItemStatusDTO> GetItemStatusById(int id);

        Task<bool> CreateItemStatus(ItemStatusAddDTO itemStatusDto);
    }
}
