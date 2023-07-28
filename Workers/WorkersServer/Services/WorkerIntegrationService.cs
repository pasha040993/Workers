using AutoMapper;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using WorkersServer.Data;
using WorkersServer.Models.POCOs;

namespace WorkersServer.Services
{
    public class WorkerIntegrationService : WorkerIntegration.WorkerIntegrationBase
    {
        private readonly ILogger<WorkerIntegrationService> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public WorkerIntegrationService(ILogger<WorkerIntegrationService> logger, IMapper mapper, ApplicationDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public override async Task<WorkerAction> CreateWorker(WorkerMessage request, ServerCallContext context)
        {
            try
            {
                var worker = _mapper.Map<Worker>(request);
                await _context.Workers.AddAsync(worker);
                await _context.SaveChangesAsync();
                var result = new WorkerAction()
                {
                    ActionType = Action.Create,
                    Worker = _mapper.Map<WorkerMessage>(worker)
                };
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error {nameof(CreateWorker)}");
                throw;
            }
        }

        public override async Task<WorkerAction> DeleteWorker(DeleteWorkerRequest request, ServerCallContext context)
        {
            try
            {
                var id = Guid.Parse(request.Id);
                var worker = await _context.Workers.FindAsync(id);
                if (worker == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Worker not found"));
                }

                _context.Workers.Remove(worker);
                await _context.SaveChangesAsync();
                var result = new WorkerAction()
                {
                    ActionType = Action.Delete,
                    Worker = _mapper.Map<WorkerMessage>(worker)
                };
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error {nameof(CreateWorker)}");
                throw;
            }
        }

        public override async Task<ListWorker> ListWorkers(EmptyMessage request, ServerCallContext context)
        {
            try
            {
                var listWorker = new ListWorker();
                var workers = await _context.Workers.ToArrayAsync();
                listWorker.Workers.AddRange(workers.Select(w => _mapper.Map<WorkerMessage>(w)));
                return await Task.FromResult(listWorker);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error {nameof(CreateWorker)}");
                throw;
            }
        }

        public override async Task<WorkerAction> UpdateWorker(WorkerMessage request, ServerCallContext context)
        {
            try
            {
                var requestWorker = _mapper.Map<Worker>(request);
                var worker = await _context.Workers.FindAsync(requestWorker.Id);
                if (worker == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Worker not found"));
                }

                worker.FirstName = requestWorker.FirstName;
                worker.LastName = requestWorker.LastName;
                worker.MiddleName = requestWorker.MiddleName;
                worker.Birthday = requestWorker.Birthday;
                worker.Sex = requestWorker.Sex;
                worker.HaveChildren = requestWorker.HaveChildren;
                await _context.SaveChangesAsync();

                var result = new WorkerAction()
                {
                    ActionType = Action.Update,
                    Worker = _mapper.Map<WorkerMessage>(worker)
                };
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error {nameof(CreateWorker)}");
                throw;
            }
        }
    }
}