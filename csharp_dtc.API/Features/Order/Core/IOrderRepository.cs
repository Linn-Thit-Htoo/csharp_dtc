using csharp_dtc.API.OrderDbContextModels;
using csharp_dtc.API.OrderDetailPersistence.Base;

namespace csharp_dtc.API.Features.Order.Core
{
    public interface IOrderRepository : IRepositoryBase<TblOrder>
    {
    }
}
