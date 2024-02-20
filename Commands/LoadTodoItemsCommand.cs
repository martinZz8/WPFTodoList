using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoList.DTO;
using TodoList.Models;
using TodoList.Services.TodoListServices;
using TodoList.Static;
using TodoList.ViewModels;

namespace TodoList.Commands
{
    internal class LoadTodoItemsCommand : AsyncCommandBase
    {
        private readonly ListItemsViewModel _listItemsViewModel;
        private readonly ITodoListService _todoListService;

        public LoadTodoItemsCommand(ListItemsViewModel listItemsViewModel, ITodoListService todoListService)
        {
            _listItemsViewModel = listItemsViewModel;
            _todoListService = todoListService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _listItemsViewModel.IsDataLoading = true;
            _listItemsViewModel.ErrorMessage = "";

            // Load todo items
            try
            {
                IEnumerable<TodoItemDTO> fetchedTodoItemsDto = await _todoListService.GetAllTodoItems();

                //Console.WriteLine($"Length of fetched fetchedTodoItemsDto: {fetchedTodoItemsDto.Count()}");
                //ConsoleLogCollectionItems.Log(fetchedTodoItemsDto);

                _listItemsViewModel.UpdateTodoItems(fetchedTodoItemsDto);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load todo items", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _listItemsViewModel.ErrorMessage = "Error occured during fetch of todo items.";
                //Console.WriteLine(er.Message);
            }

            _listItemsViewModel.IsDataLoading = false;
        }
    }
}
