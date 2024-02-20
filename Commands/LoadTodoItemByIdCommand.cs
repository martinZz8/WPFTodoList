using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoList.DTO;
using TodoList.Services.TodoListServices;
using TodoList.ViewModels;

namespace TodoList.Commands
{
    internal class LoadTodoItemByIdCommand : AsyncCommandBase
    {
        private readonly UpdateItemViewModel _updateItemViewModel;
        private readonly ITodoListService _todoListService;

        public LoadTodoItemByIdCommand(UpdateItemViewModel updateItemViewModel, ITodoListService todoListService)
        {
            _updateItemViewModel = updateItemViewModel;
            _todoListService = todoListService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // Load todo item by id
            if (_updateItemViewModel.PreviouslySelectedTodoItemVMM != null)
            {
                try
                {
                    TodoItemDTO fetchedTodoItemDto = await _todoListService.GetTodoItem(_updateItemViewModel.PreviouslySelectedTodoItemVMM.Id);

                    //Console.WriteLine($"fetched item id: {fetchedTodoItemDto.Id}; name: {fetchedTodoItemDto.Name}; status name: {fetchedTodoItemDto.Status.Name}");

                    _updateItemViewModel.UpdateTodoItem(fetchedTodoItemDto);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to load item statuses", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //Console.WriteLine(er.Message);
                }
            }
            else
            {
                MessageBox.Show("There wasn't selected item for update", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
