// See https://aka.ms/new-console-template for more information

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using IPAddressError;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var database = new TestcontainersBuilder<PostgreSqlTestcontainer>()
    .WithDatabase(new PostgreSqlTestcontainerConfiguration("postgres:12.11-alpine")
    {
        Database = "db",
        Username = "postgres",
        Password = "postgres",
    })
    .Build();

await database.StartAsync();

try
{
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddLogging(builder => builder.AddConsole());
    serviceCollection.AddDbContext<ExampleDbContext>(builder => builder.UseNpgsql(database.ConnectionString));
    var provider = serviceCollection.BuildServiceProvider();

    await using var scope = provider.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ExampleDbContext>();

    const int subnet = 16;
    
    var queryable = dbContext.Entities
        .Select(x => EF.Functions.Network(EF.Functions.SetMaskLength(x.IpAddress, subnet)));

    var count = await queryable
        .Where(x=>queryable.Contains(x))
        // .Join(queryable, x => x, x => x, (tuple, valueTuple) => tuple)
        .ToListAsync();
        
    Console.WriteLine(count);
}
finally
{
    await database.DisposeAsync();
}


