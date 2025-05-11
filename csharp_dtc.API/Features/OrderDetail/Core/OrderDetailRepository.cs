using csharp_dtc.API.OrderDetailDbContextModels;
using csharp_dtc.API.OrderDetailPersistence.Base;
using Microsoft.EntityFrameworkCore;

namespace csharp_dtc.API.Features.OrderDetail.Core;

public class OrderDetailRepository : RepositoryBase<TblOrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(DbContext context)
        : base(context) { }
}
