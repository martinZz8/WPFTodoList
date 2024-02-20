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
    internal class UpdateTodoItemCommand : AsyncCommandBase
    {
        private readonly UpdateItemViewModel _updateItemViewModel;
        private readonly NavigationService _navigationService;
        private readonly ITodoListService _todoListService;

        public UpdateTodoItemCommand(UpdateItemViewModel updateItemViewModel, NavigationService navigationService, ITodoListService todoListService)
        {
            _updateItemViewModel = updateItemViewModel;
            _navigationService = navigationService;
            _todoListService = todoListService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // Update specified todo item
            if (_updateItemViewModel.Name.Length > 0 && _updateItemViewModel.SelectedItemStatusVMM != null)
            {
                try
                {
                    TodoItemAddDTO todoItemToAdd = new TodoItemAddDTO(_updateItemViewModel.Name, _updateItemViewModel.SelectedItemStatusVMM.Id);

                    TodoItemDTO retItem = await _todoListService.UpdateTodoItem(todoItemToAdd, _updateItemViewModel.PreviouslySelectedTodoItemVMM.Id);

                    if (retItem != null)
                    {
                        MessageBox.Show($"Successfully updated todo item of id {_updateItemViewModel.PreviouslySelectedTodoItemVMM.Id}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        _navigationService.Navigate();
                    }
                    else
                    {
                        MessageBox.Show($"Wrong input while updating todo item of id {_updateItemViewModel.PreviouslySelectedTodoItemVMM.Id} (or item doesn't exist)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to update todo item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //Console.WriteLine(er.Message);
                }
            }
            else
            {
                MessageBox.Show("Name and status need to be specified", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
