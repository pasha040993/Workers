using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WorkersClient
{
    public class GrpcServiceProvider : IDisposable
    {
        public GrpcServiceProvider(string serviceUrl)
        {
            this._serviceUrl = serviceUrl;
            this._defaultRpcChannel = new Lazy<GrpcChannel>(GrpcChannel.ForAddress(this._serviceUrl));
        }

        public WorkerIntegration.WorkerIntegrationClient GetClient() => this._client ??= new WorkerIntegration.WorkerIntegrationClient(this._defaultRpcChannel.Value);

        #region IDisposable Support
        private bool _disposedValue; 

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (this._defaultRpcChannel.IsValueCreated)
                    {
                        this._defaultRpcChannel.Value.Dispose();
                    }
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support

        private Lazy<GrpcChannel> _defaultRpcChannel { get; set; }
        private string _serviceUrl { get; set; }
        private WorkerIntegration.WorkerIntegrationClient _client { get; set; }
    }
}
