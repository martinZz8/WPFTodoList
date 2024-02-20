using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoList.Commands;
using TodoList.Services;
using TodoList.Services.TodoListServices;
using TodoList.ViewModels.Models;
using TodoList.Wrappers;

namespace TodoList.ViewModels
{
    internal class DeleteItemViewModel: ViewModelBase
    {
        private readonly ITodoListService _todoListService;

        private readonly SelectTodoItem _previouslySelectedTodoItem;
        public TodoItemVMM PreviouslySelectedTodoItemVMM => _previouslySelectedTodoItem?.Item;

        public string GetTitleName => $"Delete todo item with id: {(PreviouslySelectedTodoItemVMM != null ? PreviouslySelectedTodoItemVMM.Id.ToString() : "-")}";

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        public DeleteItemViewModel(SelectTodoItem previouslySelectedTodoItem, NavigationService listAllTodoItemsNavigationService, ITodoListService todoListService)
        {
            // Instantiate services
            _todoListService = todoListService;

            _previouslySelectedTodoItem = previouslySelectedTodoItem;

            DeleteCommand = new DeleteTodoItemCommand(this, listAllTodoItemsNavigationService, _todoListService);
            CancelCommand = new NavigateCommand(listAllTodoItemsNavigationService);
        }

        // You don't have to use this builder, but can (instead of pure constructor)
        public static DeleteItemViewModel BuildViewModel(SelectTodoItem selectedTodoItem, NavigationService listAllTodoItemsNavigationService, ITodoListService todoListService)
        {
            DeleteItemViewModel viewModel = new DeleteItemViewModel(selectedTodoItem, listAllTodoItemsNavigationService, todoListService);

            return viewModel;
        }
    }
}
