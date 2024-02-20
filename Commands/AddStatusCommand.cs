using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoList.DTO;
using TodoList.Services;
using TodoList.Services.ItemStatusServices;
using TodoList.ViewModels;

namespace TodoList.Commands
{
    internal class AddStatusCommand: AsyncCommandBase
    {
        private readonly AddStatusViewModel _addStatusViewModel;
        private readonly NavigationService _navigationService;
        private readonly IItemStatusService _itemStatusService;

        public AddStatusCommand(AddStatusViewModel addStatusViewModel, NavigationService navigationService, IItemStatusService itemStatusService)
        {
            _addStatusViewModel = addStatusViewModel;
            _navigationService = navigationService;
            _itemStatusService = itemStatusService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // Validate data ('Name' need to be specified)
            if (_addStatusViewModel.Name.Length > 0)
            {
                ItemStatusAddDTO dto = new ItemStatusAddDTO(_addStatusViewModel.Name);

                try
                {
                    bool res = await _itemStatusService.CreateItemStatus(dto);

                    if (res)
                    {
                        MessageBox.Show("Successfully created status", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        _navigationService.Navigate();
                    }
                    else
                    {
                        MessageBox.Show("Already exist status with given name. Try another one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _addStatusViewModel.Name = "";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to create new status", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //Console.WriteLine(es.Message);
                }
            }
            else
            {
                MessageBox.Show("Name need to be specified", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
