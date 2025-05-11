using csharp_dtc.API.OrderDbContextModels;
using csharp_dtc.API.OrderDetailPersistence.Base;
using Microsoft.EntityFrameworkCore;

namespace csharp_dtc.API.Features.Order.Core;

public class OrderRepository : RepositoryBase<TblOrder>, IOrderRepository
{
    public OrderRepository(DbContext context)
        : base(context) { }
}
