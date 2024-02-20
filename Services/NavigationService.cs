using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Commands;
using TodoList.Store;
using TodoList.ViewModels;

namespace TodoList.Services
{
    internal class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            //Console.WriteLine($"{nameof(NavigateCommand)} clicked");
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
