using csharp_dtc.API.Features.OrderDetail.Core;

namespace csharp_dtc.API.OrderDetailPersistence.Wrapper;

public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cs = default);
    IOrderDetailRepository OrderDetailRepository { get; }
}
