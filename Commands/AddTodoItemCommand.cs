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
    internal class AddTodoItemCommand : AsyncCommandBase
    {
        private readonly AddItemViewModel _addItemViewModel;
        private readonly NavigationService _navigationService;
        private readonly ITodoListService _todoListService;

        public AddTodoItemCommand(AddItemViewModel addItemViewModel, NavigationService navigationService, ITodoListService todoListService)
        {
            _addItemViewModel = addItemViewModel;
            _navigationService = navigationService;
            _todoListService = todoListService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            //Console.WriteLine($"{nameof(AddTodoItemCommand)} clicked");

            // Validate data ('Name' and 'SelectedItemStatusVMM' need to be specified)
            if (_addItemViewModel.Name.Length > 0 && _addItemViewModel.SelectedItemStatusVMM != null)
            {
                TodoItemAddDTO dto = new TodoItemAddDTO(_addItemViewModel.Name, _addItemViewModel.SelectedItemStatusVMM.Id);

                try
                {
                    bool res = await _todoListService.CreateTodoItem(dto);

                    if (res)
                    {
                        MessageBox.Show("Successfully created todo item", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        _navigationService.Navigate();
                    }
                    else
                    {
                        MessageBox.Show("Wrong input while creating new todo item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to create new todo item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //Console.WriteLine(es.Message);
                }
            }
            else
            {
                MessageBox.Show("Name and status need to be specified", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
