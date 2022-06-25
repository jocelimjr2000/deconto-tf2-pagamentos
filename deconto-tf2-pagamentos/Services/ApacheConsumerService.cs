using Confluent.Kafka;
using deconto_tf2_pagamentos.DTO;
using System.Text.Json;
using System.Threading.Tasks;

namespace deconto_tf2_pagamentos.Services
{
    public class ApacheConsumerService : IHostedService
    {
        private readonly string _topic = "teste";
        private readonly string _groupId = "grupo_teste";
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly PagamentoService _pagamentoService;

        public ApacheConsumerService(PagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            try
            {
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


                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
