using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkersWpfClient.Interface;
using WorkersWpfClient.View;
using WorkersWpfClient.ViewModels.Commands;

namespace WorkersWpfClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IWorkerService _workerService;

        private bool _isRefreshState = false;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }


        private ObservableCollection<WorkerViewModel> _workers = new ObservableCollection<WorkerViewModel>();
        public ObservableCollection<WorkerViewModel> Workers
        {
            get => _workers;
            set => SetProperty(ref _workers, value);
        }

        public WorkerViewModel SelectedWorker { get; set; }

        public MainViewModel(IWorkerService workerService)
        {
            _workerService = workerService;
            RefreshCommand = new AsyncCommand(OnRefreshCommand, o => !_isRefreshState);
            DeleteCommand = new AsyncCommand(OnDeleteCommand, o => SelectedWorker != null);
            EditCommand = new AsyncCommand(OnEditCommand, o => SelectedWorker != null);
            AddCommand = new AsyncCommand(OnAddCommand, o => !_isRefreshState);
        }

        private async Task OnRefreshCommand(object arg)
        {
            _isRefreshState = true;
            try
            {
                Workers = new ObservableCollection<WorkerViewModel>(await _workerService.GetAllWorkers());
                OnPropertyChanged(nameof(Workers));
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при обновлении данных.");
            }
            finally
            {
                _isRefreshState = false;
            }
        }

        private async Task OnDeleteCommand(object arg)
        {
            var res = await _workerService.DeleteWorker(SelectedWorker);
            if (res)
            {
                await Application.Current.Dispatcher.BeginInvoke(new System.Action(() => Workers.Remove(SelectedWorker)));
            }
            else
            {
                MessageBox.Show("Ошибка при удалении.");
            }
        }

        private async Task OnEditCommand(object arg)
        {
            await Application.Current.Dispatcher.BeginInvoke(new System.Action(async () =>
            {
                var view = new WorkerWindow(SelectedWorker);
                if (view.ShowDialog() == true)
                {
                    var editWorker = view.Worker;
                    var res = await _workerService.UpdaterWorker(editWorker);
                    if (res)
                    {
                        SelectedWorker.CopyProperties(editWorker);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении.");
                    }
                }
            }));
        }
        
        private async Task OnAddCommand(object arg)
        {
            await Application.Current.Dispatcher.BeginInvoke(new System.Action(async () =>
            {
                var view = new WorkerWindow();
                if (view.ShowDialog() == true)
                {
                    var (res, addWorker) = await _workerService.AddWorker(view.Worker);
                    if (res)
                    {
                        Workers.Add(addWorker);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении.");
                    }
                }
            }));
        }
    }
}
