using Clean.Architecture.Core.Domain.Entities;
using Clean.Architecture.Infrastructure.Persistence.Data.Config.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.Infrastructure.Persistence.Data.Config;

/// <summary>
/// Configuration class for the Message entity
/// </summary>
public sealed class MessageConfiguration : BaseEntityConfiguration<Message>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Message> builder)
    {
        // Call base implementation for soft-delete setup
        base.Configure(builder);
        // Enforce that the text can be no longer than 100 characters and is required
        builder.Property(m => m.MessageText)
            .HasMaxLength(100)
            .IsRequired();
    }
}