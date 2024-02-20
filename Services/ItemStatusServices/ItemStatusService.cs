using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TodoList.DbContexts;
using TodoList.DTO;
using TodoList.Models;

namespace TodoList.Services.ItemStatusServices
{
    internal class ItemStatusService: IItemStatusService
    {
        private readonly TodoListDbContext _dbContext;
        
        public ItemStatusService(string connectionString)
        {
            _dbContext = new TodoListDbContextFactory(connectionString).CreateDbContext();
        }

        public async Task<IEnumerable<ItemStatusDTO>> GetAllItemStatuses()
        {
            IEnumerable<ItemStatus> itemStatuses = await _dbContext.ItemStatus.ToListAsync();

            return itemStatuses.Select(it => new ItemStatusDTO(it));
        }

        public async Task<ItemStatusDTO> GetItemStatusByName(string name)
        {
            ItemStatus itemStatus = await _dbContext.ItemStatus.FirstOrDefaultAsync(it => it.Name == name);

            if (itemStatus != null)
            {
                ItemStatusDTO itemStatusToRet = new ItemStatusDTO(itemStatus);

                return itemStatusToRet;
            }

            return null;
        }

        public async Task<ItemStatusDTO> GetItemStatusById(int id)
        {
            ItemStatus itemStatus = await _dbContext.ItemStatus.FirstOrDefaultAsync(it => it.Id == id);

            if (itemStatus != null)
            {
                ItemStatusDTO itemStatusToRet = new ItemStatusDTO(itemStatus);

                return itemStatusToRet;
            }

            return null;
        }

        public async Task<bool> CreateItemStatus(ItemStatusAddDTO itemStatusDto)
        {
            ItemStatus foundItemStatus = await _dbContext.ItemStatus.FirstOrDefaultAsync(it => it.Name == itemStatusDto.Name);

            if (foundItemStatus == null)
            {
                ItemStatus itemStatusToAdd = new ItemStatus()
                {
                    Name = itemStatusDto.Name
                };

                await _dbContext.ItemStatus.AddAsync(itemStatusToAdd);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
