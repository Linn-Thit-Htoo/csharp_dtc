using csharp_dtc.API.Features.OrderDetail.Core;
using csharp_dtc.API.OrderDetailDbContextModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace csharp_dtc.API.OrderDetailPersistence.Wrapper;

public class UnitOfWork : IUnitOfWork
{
    internal readonly DbContext _context;
    internal readonly string? _currentUser = "SYSTEM";

    public UnitOfWork(OrderDetailDbContext orderDetailDbContext)
    {
        _context = orderDetailDbContext;
        OrderDetailRepository = new OrderDetailRepository(orderDetailDbContext);
    }

    public void SaveChanges()
    {
        var modifiedEntries = _context
            .ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added)
            .ToList();

        foreach (var entry in modifiedEntries)
        {
            Type type = entry.Entity.GetType();

            if (entry.State == EntityState.Added)
            {
                PropertyInfo createdBy = type.GetProperty("CreatedBy")!;
                createdBy?.SetValue(entry.Entity, _currentUser);

                PropertyInfo createdDate = type.GetProperty("CreatedAt")!;
                createdDate?.SetValue(entry.Entity, DateTime.Now);
            }

            if (entry.State == EntityState.Modified)
            {
                PropertyInfo modifiedBy = type.GetProperty("ModifiedBy")!;
                modifiedBy?.SetValue(entry.Entity, _currentUser);

                PropertyInfo modifiedAt = type.GetProperty("ModifiedAt")!;
                modifiedAt?.SetValue(entry.Entity, DateTime.Now);
            }
        }

        _context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cs = default)
    {
        var modifiedEntries = _context
            .ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added)
            .ToList();

        foreach (var entry in modifiedEntries)
        {
            Type type = entry.Entity.GetType();

            if (entry.State == EntityState.Added)
            {
                PropertyInfo createdBy = type.GetProperty("CreatedBy")!;
                createdBy?.SetValue(entry.Entity, _currentUser);

                PropertyInfo createdDate = type.GetProperty("CreatedAt")!;
                createdDate?.SetValue(entry.Entity, DateTime.Now);
            }

            if (entry.State == EntityState.Modified)
            {
                PropertyInfo modifiedBy = type.GetProperty("ModifiedBy")!;
                modifiedBy?.SetValue(entry.Entity, _currentUser);

                PropertyInfo modifiedAt = type.GetProperty("ModifiedAt")!;
                modifiedAt?.SetValue(entry.Entity, DateTime.Now);
            }
        }

        await _context.SaveChangesAsync(cs);
    }

    public IOrderDetailRepository OrderDetailRepository { get; set; }
}
