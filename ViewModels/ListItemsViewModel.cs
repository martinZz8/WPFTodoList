using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoList.Commands;
using TodoList.DTO;
using TodoList.Models;
using TodoList.Services;
using TodoList.Services.TodoListServices;
using TodoList.Store;
using TodoList.ViewModels.Models;
using TodoList.Wrappers;

namespace TodoList.ViewModels
{
    internal class ListItemsViewModel: ViewModelBase
    {
		// TodoListService instantion
		private readonly ITodoListService _todoListService;

		// Fetched todo items
		private readonly ObservableCollection<TodoItemVMM> _todoItems;
		public IEnumerable<TodoItemVMM> TodoItems => _todoItems;

        // Selected todo item instantion
        private SelectTodoItem _selectedTodoItem;
		public TodoItemVMM SelectedTodoItemVMM
        {
			get
			{
				return _selectedTodoItem.Item;
			}
			set
			{
                _selectedTodoItem.Item = value;
				OnPropertyChanged(nameof(SelectedTodoItemVMM));
                //Console.WriteLine($"change of selected todo item; it's id: {_selectedTodoItem.Item.Id}");
            }
		}

        private bool _isDataLoading;
        public bool IsDataLoading
        {
            get
            {
                return _isDataLoading;
            }

            set
            {
                _isDataLoading = value;
                OnPropertyChanged(nameof(IsDataLoading));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(IsError));
            }
        }

        public bool IsError => !string.IsNullOrEmpty(_errorMessage);

        // Commands
        public ICommand LoadTodoItemsCommand { get; }
		public ICommand AddItemCommand { get; }
        public ICommand UpdateItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand AddStatusCommand { get; }

        public ListItemsViewModel(
            SelectTodoItem selectedTodoItem,
            NavigationService addItemNavigationService,
            NavigationService updateItemNavigationService,
            NavigationService deleteItemNavigationService,
            NavigationService addStatusNavigationService,
            ITodoListService todoListService
        )
        {
            // Instantiate todoItems service
            _todoItems = new ObservableCollection<TodoItemVMM>();

            // Instantiate todoList service
            _todoListService = todoListService;

            // Properties
            _selectedTodoItem = selectedTodoItem;
            SelectedTodoItemVMM = null;
            IsDataLoading = false;

            // Commands
            LoadTodoItemsCommand = new LoadTodoItemsCommand(this, _todoListService);
            AddItemCommand = new NavigateCommand(addItemNavigationService);
            UpdateItemCommand = new CondListNavigateCommand(this, updateItemNavigationService); //old: new NavigateCommand(updateItemNavigationService);
            DeleteItemCommand = new CondListNavigateCommand(this, deleteItemNavigationService);
            AddStatusCommand = new NavigateCommand(addStatusNavigationService);
        }

        // Use this builder to get ListItemsViewModel with fetched data (rather than calling pure constructor)
        public static ListItemsViewModel BuildViewModel(
            SelectTodoItem selectedTodoItemId,
            NavigationService addItemNavigationService,
            NavigationService updateItemNavigationService,
            NavigationService deleteItemNavigationService,
            NavigationService addStatusNavigationService,
            ITodoListService todoListService
        )
        {
            ListItemsViewModel viewModel = new ListItemsViewModel(
                selectedTodoItemId,
                addItemNavigationService,
                updateItemNavigationService,
                deleteItemNavigationService,
                addStatusNavigationService,
                todoListService
            );

            viewModel.LoadTodoItemsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateTodoItems(IEnumerable<TodoItemDTO> todoItemsDto)
        {
            _todoItems.Clear();

            foreach (TodoItemDTO it in todoItemsDto)
            {
                _todoItems.Add(new TodoItemVMM(it));
            }

            //IsDataLoading = false;
            //Console.WriteLine($"Length of fetched data: {_todoItems.Count}");
        }
    }
}
