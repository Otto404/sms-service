using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BAL.Scheduling
{
    public class MailingService : IHostedService
    {
        private Task executingTask;
        private readonly CancellationTokenSource stoppingCts =
            new CancellationTokenSource();

        private readonly IServiceScopeFactory serviceScopeFactory;

        public MailingService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            executingTask = ExecuteAsync(stoppingCts.Token);
            if (executingTask.IsCompleted)
            {
                return executingTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (executingTask == null)
            {
                return;
            }

            try
            {
                stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        private async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                await ProcessAsync();

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task ProcessAsync()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                await ProcessInScopeAsync(scope.ServiceProvider);
            }
        }

        private async Task ProcessInScopeAsync(IServiceProvider serviceProvider)
        {
            var mailing = serviceProvider.GetService<Mailing>();
            await mailing.Execute();
        }
    }
}
