using Clean.Architecture.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Infrastructure.Persistence.Data.Seeds;

/// <summary>
/// Seed class to seed the database with message(s)
/// </summary>
public static class MessageSeed
{
    /// <summary>
    /// Seeds the database in an asynchronous manner
    /// </summary>
    /// <param name="context"><see cref="EfContext"/> to seed with data</param>
    public static async Task SeedAsync(AppDbContext context)
    {
        // Construct new Message instance
        var message = new Message { MessageText = "This message was retrieved from the database!" };

        if (!await context.Messages.AnyAsync())
        {
            // If no messages are present, add the message and save
            context.Messages.Add(message);
            await context.SaveChangesAsync();
        }
    }
}