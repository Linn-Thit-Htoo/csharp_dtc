using csharp_dtc.API.Extensions;
using System.Transactions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder);
TransactionManager.ImplicitDistributedTransactions = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
