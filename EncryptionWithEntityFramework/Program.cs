using EncryptionWithEntityFramework.Domain.Users;
using EncryptionWithEntityFramework.Infrastructure.Persistence;
using EncryptionWithEntityFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Register(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/", async (
    ApplicationDbContext context,
    User user,
    CancellationToken cancellationToken) =>
{
    context.Users.Add(user);
    await context.SaveChangesAsync(cancellationToken);

    return user.Id;
})
.WithName("PostUser")
.WithOpenApi();

app.MapGet("/", (
    ApplicationDbContext context,
    CancellationToken cancellationToken) =>
{
    return context.Users.ToListAsync(cancellationToken);
})
.WithName("GetUsers")
.WithOpenApi();

app.Run();