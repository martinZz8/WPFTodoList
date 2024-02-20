using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TodoList.Services;
using TodoList.Store;
using TodoList.ViewModels;
using TodoList.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Services.TodoListServices;
using TodoList.Services.ItemStatusServices;
using Microsoft.Extensions.Configuration;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly SelectTodoItem _selectedTodoItem;
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");

                    services.AddSingleton<ITodoListService, TodoListService>((s) => new TodoListService(connectionString));
                    services.AddSingleton<IItemStatusService, ItemStatusService>((s) => new ItemStatusService(connectionString));
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<SelectTodoItem>();
                })
                .Build();

            _navigationStore = _host.Services.GetService<NavigationStore>();
            _selectedTodoItem = _host.Services.GetService<SelectTodoItem>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            _navigationStore.CurrentViewModel = CreateListItemsViewModel(); //old: new ListItemsViewModel(_navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private ListItemsViewModel CreateListItemsViewModel()
        {
            // Note! - we can pass here more arguments, if view requires them (e.g. data that is shared between multiple viewModels)
            return ListItemsViewModel.BuildViewModel(
                _selectedTodoItem,
                new NavigationService(_navigationStore, CreateAddItemViewModel),
                new NavigationService(_navigationStore, CreateUpdateItemViewModel),
                new NavigationService(_navigationStore, CreateDeleteItemViewModel),
                new NavigationService(_navigationStore, CreateAddStatusViewModel),
                _host.Services.GetService<ITodoListService>()
            );
        }

        private AddItemViewModel CreateAddItemViewModel()
        {
            return AddItemViewModel.BuildViewModel(
                new NavigationService(_navigationStore, CreateListItemsViewModel),
                _host.Services.GetService<ITodoListService>(),
                _host.Services.GetService<IItemStatusService>()
            );
        }

        private UpdateItemViewModel CreateUpdateItemViewModel()
        {
            return UpdateItemViewModel.BuildViewModel(
                _selectedTodoItem,
                new NavigationService(_navigationStore, CreateListItemsViewModel),
                _host.Services.GetService<ITodoListService>(),
                _host.Services.GetService<IItemStatusService>()
            );
        }

        private DeleteItemViewModel CreateDeleteItemViewModel()
        {
            return DeleteItemViewModel.BuildViewModel(
                _selectedTodoItem,
                new NavigationService(_navigationStore, CreateListItemsViewModel),
                _host.Services.GetService<ITodoListService>()
            );
        }

        private AddStatusViewModel CreateAddStatusViewModel()
        {
            return AddStatusViewModel.BuildViewModel(
                new NavigationService(_navigationStore, CreateListItemsViewModel),
                _host.Services.GetService<IItemStatusService>()
            );
        }
    }
}
