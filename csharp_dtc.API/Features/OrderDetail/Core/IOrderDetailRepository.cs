using csharp_dtc.API.OrderDetailDbContextModels;
using csharp_dtc.API.OrderDetailPersistence.Base;

namespace csharp_dtc.API.Features.OrderDetail.Core;

public interface IOrderDetailRepository : IRepositoryBase<TblOrderDetail>
{
}
