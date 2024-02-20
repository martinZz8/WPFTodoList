using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoList.DTO;
using TodoList.Services;
using TodoList.Services.TodoListServices;
using TodoList.ViewModels;

namespace TodoList.Commands
{
    internal class DeleteTodoItemCommand : AsyncCommandBase
    {
        private readonly DeleteItemViewModel _deleteItemViewModel;
        private readonly NavigationService _navigationService;
        private readonly ITodoListService _todoListService;

        public DeleteTodoItemCommand(DeleteItemViewModel deleteItemViewModel, NavigationService navigationService, ITodoListService todoListService)
        {
            _deleteItemViewModel = deleteItemViewModel;
            _navigationService = navigationService;
            _todoListService = todoListService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_deleteItemViewModel.PreviouslySelectedTodoItemVMM != null)
            {
                try
                {
                    bool ret = await _todoListService.DeleteTodoItem(_deleteItemViewModel.PreviouslySelectedTodoItemVMM.Id);

                    if (ret)
                    {
                        MessageBox.Show($"Successfully deleted todo item of id {_deleteItemViewModel.PreviouslySelectedTodoItemVMM.Id}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        _navigationService.Navigate();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete - todo item of id {_deleteItemViewModel.PreviouslySelectedTodoItemVMM.Id} doesn't exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to delete todo item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //Console.WriteLine(er.Message);
                }
            }
            else
            {
                MessageBox.Show("There wasn't selected item for deletion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
