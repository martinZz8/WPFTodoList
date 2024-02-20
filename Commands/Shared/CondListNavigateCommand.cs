using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Services;
using TodoList.ViewModels;

namespace TodoList.Commands
{
    internal class CondListNavigateCommand: NavigateCommand
    {
        private readonly ListItemsViewModel _viewModel;

        public CondListNavigateCommand(ListItemsViewModel viewModel, NavigationService navigationService) : base(navigationService)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _viewModel.SelectedTodoItemVMM != null;
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedTodoItemVMM))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
