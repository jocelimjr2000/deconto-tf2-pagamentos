using Confluent.Kafka;
using deconto_tf2_pagamentos.DTO;
using System.Text.Json;

namespace deconto_tf2_pagamentos.Services
{
    public class ApacheConsumerService : BackgroundService
    {
        private readonly string _topic = "pagamentos";
        private readonly string _groupId = "grupo_teste";
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly IServiceProvider _serviceProvider;

        public ApacheConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await DoWorkAsync(cancellationToken);
        }
        public async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            using(var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumerBuilder.Subscribe(_topic);
                var cancelToken = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var consumer = consumerBuilder.Consume(cancelToken.Token);

                        var pagamento = JsonSerializer.Deserialize<PagamentoDTO>(consumer.Message.Value);

                        if(pagamento != null)
                        {
                            using (var scope =_serviceProvider.CreateScope())
                            {
                                var pagamentoService = scope.ServiceProvider.GetRequiredService<PagamentoService>();

                                await pagamentoService.Cadastrar(pagamento);
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumerBuilder.Close();
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }
}
