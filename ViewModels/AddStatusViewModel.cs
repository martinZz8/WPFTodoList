using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoList.Commands;
using TodoList.Services;
using TodoList.Services.ItemStatusServices;
using TodoList.Wrappers;

namespace TodoList.ViewModels
{
    internal class AddStatusViewModel: ViewModelBase
    {
        private readonly IItemStatusService _itemStatusesService;

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
            }
        }

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public AddStatusViewModel(NavigationService listAllTodoItemsNavigationService, IItemStatusService itemStatusService)
        {
            // Instantiate services
            _itemStatusesService = itemStatusService;

            AddCommand = new AddStatusCommand(this, listAllTodoItemsNavigationService, _itemStatusesService);
            CancelCommand = new NavigateCommand(listAllTodoItemsNavigationService);
        }

        // You don't have to use this builder, but can (instead of pure constructor)
        public static AddStatusViewModel BuildViewModel(NavigationService listAllTodoItemsNavigationService, IItemStatusService itemStatusService)
        {
            AddStatusViewModel viewModel = new AddStatusViewModel(listAllTodoItemsNavigationService, itemStatusService);

            return viewModel;
        }
    }
}
