using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkersWpfClient.ViewModels;

namespace WorkersWpfClient.Interface
{
    public interface IWorkerService
    {
        Task<(bool, WorkerViewModel)> AddWorker(WorkerViewModel worker);
        Task<bool> DeleteWorker(WorkerViewModel worker);
        Task<IEnumerable<WorkerViewModel>> GetAllWorkers();
        Task<bool> UpdaterWorker(WorkerViewModel worker);
    }
}
