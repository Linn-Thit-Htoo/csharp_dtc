using csharp_dtc.API.Features.Order.Core;

namespace csharp_dtc.API.OrderPersistence.Wrapper
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cs = default);
        IOrderRepository OrderRepository { get; }
    }
}
