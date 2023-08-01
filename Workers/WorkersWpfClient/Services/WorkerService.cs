using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorkersWpfClient.Interface;
using WorkersWpfClient.Models;
using WorkersWpfClient.ViewModels;
using Microsoft.Extensions.Logging;
using WorkersClient;

namespace WorkersWpfClient.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings = new();

        public WorkerService(ILogger<WorkerService> logger, IConfiguration configuration, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            configuration.GetSection("AppSettings").Bind(_appSettings);
        }

        public async Task<(bool, WorkerViewModel)> AddWorker(WorkerViewModel worker)
        {
            try
            {
                using var serviceProvider = new GrpcServiceProvider(_appSettings.ServerEndpoint);
                var client = serviceProvider.GetClient();
                var res = await client.CreateWorkerAsync(_mapper.Map<WorkerMessage>(worker));
                return (true, _mapper.Map<WorkerViewModel>(res.Worker));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error {nameof(AddWorker)}");
                return (false, new WorkerViewModel());
            }
        }

        public async Task<bool> DeleteWorker(WorkerViewModel worker)
        {
            try
            {
                using var serviceProvider = new GrpcServiceProvider(_appSettings.ServerEndpoint);
                var client = serviceProvider.GetClient();
                await client.DeleteWorkerAsync(new DeleteWorkerRequest()
                {
                    Id = worker.Id.ToString()
                });
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error {nameof(DeleteWorker)}");
                return false;
            }
        }

        public async Task<IEnumerable<WorkerViewModel>> GetAllWorkers()
        {
            using var serviceProvider = new GrpcServiceProvider(_appSettings.ServerEndpoint);
            var client = serviceProvider.GetClient();
            var reply = await client.ListWorkersAsync(new EmptyMessage());
            return reply.Workers.Select(w => _mapper.Map<WorkerViewModel>(w));
        }

        public async Task<bool> UpdaterWorker(WorkerViewModel worker)
        {
            try
            {
                using var serviceProvider = new GrpcServiceProvider(_appSettings.ServerEndpoint);
                var client = serviceProvider.GetClient();
                await client.UpdateWorkerAsync(_mapper.Map<WorkerMessage>(worker));
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error {nameof(UpdaterWorker)}");
                return false;
            }
        }
    }
}
