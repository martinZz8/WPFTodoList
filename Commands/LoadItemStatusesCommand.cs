using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoList.DTO;
using TodoList.Services.ItemStatusServices;
using TodoList.Static;
using TodoList.ViewModels.Interfaces;

namespace TodoList.Commands
{
    internal class LoadItemStatusesCommand : AsyncCommandBase
    {
        private readonly IUpdatableViewModel<ItemStatusDTO> _updatableViewModel;
        private readonly IItemStatusService _itemStatusService;

        public LoadItemStatusesCommand(IUpdatableViewModel<ItemStatusDTO> updatableViewModel, IItemStatusService itemStatusService)
        {
            _updatableViewModel = updatableViewModel;
            _itemStatusService = itemStatusService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // Load item statuses
            try
            {
                IEnumerable<ItemStatusDTO> fetchedItemStatusesDto = await _itemStatusService.GetAllItemStatuses();

                //Console.WriteLine($"Length of fetched fetchedItemStatusesDto: {fetchedItemStatusesDto.Count()}");
                //ConsoleLogCollectionItems.Log(fetchedItemStatusesDto);

                _updatableViewModel.UpdateCollection(fetchedItemStatusesDto); //old: _addItemViewModel.UpdateItemStatuses(fetchedItemStatusesDto);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load item statuses", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //Console.WriteLine(er.Message);
            }
        }
    }
}
