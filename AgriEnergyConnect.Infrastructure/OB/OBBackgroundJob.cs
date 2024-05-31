using System.Data;
using AgriEnergyConnect.Application.Abstractions.Clock;
using AgriEnergyConnect.Domain.Abstractions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace AgriEnergyConnect.Infrastructure.OB;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob(
    SqlConnectionFactory sqlConnectionFactory,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider,
    IOptions<OBOptions> outboxOptions,
    ILogger<ProcessOutboxMessagesJob> logger)
    : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly OBOptions _outboxOptions = outboxOptions.Value;

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Beginning to process outbox messages");

        using var connection = sqlConnectionFactory.CreateConnection();
        using var transaction = connection.BeginTransaction();

        var outboxMessages = await GetOutboxMessagesAsync(connection, transaction);

        foreach (var outboxMessage in outboxMessages)
        {
            Exception? exception = null;

            try
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    JsonSerializerSettings)!;

                await publisher.Publish(domainEvent, context.CancellationToken);
            }
            catch (Exception caughtException)
            {
                logger.LogError(
                    caughtException,
                    "Exception while processing outbox message {MessageId}",
                    outboxMessage.Id);

                exception = caughtException;
            }

            await UpdateOutboxMessageAsync(connection, transaction, outboxMessage, exception);
        }

        transaction.Commit();

        logger.LogInformation("Completed processing outbox messages");
    }

    private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        var sql = $"""
                   SELECT TOP {_outboxOptions.BatchSize} id, content
                   FROM outbox_messages WITH (UPDLOCK, READPAST)
                   WHERE ProcessedOnUtc IS NULL
                   ORDER BY OccurredOnUtc
                   """;
        
        var outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(sql, transaction: transaction);

        return outboxMessages.ToList();
    }

    private async Task UpdateOutboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        OutboxMessageResponse outboxMessage,
        Exception? exception)
    {
        const string sql = @"
            UPDATE outbox_messages
            SET ProcessedOnUtc = @ProcessedOnUtc,
                Error = @Error
            WHERE id = @Id";

        await connection.ExecuteAsync(
            sql,
            new 
            {
                outboxMessage.Id,
                ProcessedOnUtc = dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            },
            transaction: transaction);
    }

    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}
