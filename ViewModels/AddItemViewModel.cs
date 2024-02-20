using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TodoList.Commands;
using TodoList.DTO;
using TodoList.Services;
using TodoList.Services.ItemStatusServices;
using TodoList.Services.TodoListServices;
using TodoList.ViewModels.Interfaces;
using TodoList.ViewModels.Models;
using TodoList.Wrappers;

namespace TodoList.ViewModels
{
    internal class AddItemViewModel: ViewModelBase, IUpdatableViewModel<ItemStatusDTO>
    {
        private readonly ITodoListService _todoListService;
        private readonly IItemStatusService _itemStatusesService;

        private readonly ObservableCollection<ItemStatusVMM> _availableItemStatuses;

        public IEnumerable<ItemStatusVMM> AvailableItemStatusesNames => _availableItemStatuses;

        // Input properties
        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                //Console.WriteLine($"{nameof(Name)} changed to: {_name}");
            }
        }        

        private ItemStatusVMM _selectedItemStatus;
        public ItemStatusVMM SelectedItemStatusVMM
        {
            get
            {
                return _selectedItemStatus;
            }
            set
            {
                _selectedItemStatus = value;
                OnPropertyChanged(nameof(SelectedItemStatusVMM));
                //Console.WriteLine($"change of selected item status; it's name: {_selectedItemStatus.Name}");
            }
        }

        // Commands
        public ICommand LoadItemStatusesCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }        

        public AddItemViewModel(NavigationService listAllTodoItemsNavigationService, ITodoListService todoListService, IItemStatusService itemStatusService)
        {
            // Instantiate services
            _todoListService = todoListService;
            _itemStatusesService = itemStatusService;

            _availableItemStatuses = new ObservableCollection<ItemStatusVMM>();

            LoadItemStatusesCommand = new LoadItemStatusesCommand(this, _itemStatusesService);
            AddCommand = new AddTodoItemCommand(this, listAllTodoItemsNavigationService, _todoListService);
            CancelCommand = new NavigateCommand(listAllTodoItemsNavigationService);
        }

        // Use this builder to get AddItemViewModel with fetched data (rather than calling pure constructor)
        public static AddItemViewModel BuildViewModel(NavigationService listAllTodoItemsNavigationService, ITodoListService todoListService, IItemStatusService itemStatusService)
        {
            AddItemViewModel viewModel = new AddItemViewModel(listAllTodoItemsNavigationService, todoListService, itemStatusService);

            viewModel.LoadItemStatusesCommand.Execute(null);

            return viewModel;
        }

        //public void UpdateItemStatuses(IEnumerable<ItemStatusDTO> itemStatusesDto)
        //{
        //    _availableItemStatuses.Clear();

        //    foreach (ItemStatusDTO it in itemStatusesDto)
        //    {
        //        _availableItemStatuses.Add(new ItemStatusVMM(it));
        //    }
        //}

        public void UpdateCollection(IEnumerable<ItemStatusDTO> itemStatusesDto)
        {
            _availableItemStatuses.Clear();

            foreach (ItemStatusDTO it in itemStatusesDto)
            {
                _availableItemStatuses.Add(new ItemStatusVMM(it));
            }
        }
    }
}
