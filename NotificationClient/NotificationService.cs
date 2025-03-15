using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace NotificationClient
{
    public class NotificationService : IDisposable
    {
        private HubConnection _connection;
        private const string SERVER_URL = "http://localhost:5000/notificationHub";
        private bool _isDisposed;

        public event Action<string, string, DateTime> OnMessageReceived;
        public event Action<bool> OnConnectionStatusChanged;

        public NotificationService()
        {
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(SERVER_URL)
                .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10) })
                .Build();

            _connection.Reconnecting += error =>
            {
                OnConnectionStatusChanged?.Invoke(false);
                return Task.CompletedTask;
            };

            _connection.Reconnected += connectionId =>
            {
                OnConnectionStatusChanged?.Invoke(true);
                return Task.CompletedTask;
            };

            _connection.Closed += error =>
            {
                OnConnectionStatusChanged?.Invoke(false);
                return Task.CompletedTask;
            };

            _connection.On<string, string, DateTime>("ReceiveMessage", (sender, message, timestamp) =>
            {
                OnMessageReceived?.Invoke(sender, message, timestamp);
            });
        }

        public async Task StartConnection()
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _connection.StartAsync();
                OnConnectionStatusChanged?.Invoke(true);
            }
        }

        public async Task SendMessage(string sender, string message)
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                await _connection.InvokeAsync("SendMessage", sender, message);
            }
            else
            {
                throw new InvalidOperationException("Not connected to server");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        await _connection.DisposeAsync();
                    }
                }
                _isDisposed = true;
            }
        }
    }
}
