using csharp_dtc.API.OrderDbContextModels;
using csharp_dtc.API.OrderDetailDbContextModels;
using csharp_dtc.API.OrderDetailPersistence.Wrapper;
using csharp_dtc.API.Utils;
using MediatR;
using MediatR.Wrappers;
using System.Transactions;

namespace csharp_dtc.API.Features.Order.SubmitOrder;

public class SubmitOrderHandler : IRequestHandler<SubmitOrderRequest, BaseResponse<SubmitOrderResponse>>
{
    private readonly csharp_dtc.API.OrderDetailPersistence.Wrapper.IUnitOfWork _orderDetailUnitOfWork;
    private readonly csharp_dtc.API.OrderPersistence.Wrapper.IUnitOfWork _orderUnitOfWork;

    public SubmitOrderHandler(OrderDetailPersistence.Wrapper.IUnitOfWork orderDetailUnitOfWork, OrderPersistence.Wrapper.IUnitOfWork orderUnitOfWork)
    {
        _orderDetailUnitOfWork = orderDetailUnitOfWork;
        _orderUnitOfWork = orderUnitOfWork;
    }

    public async Task<BaseResponse<SubmitOrderResponse>> Handle(SubmitOrderRequest request, CancellationToken cancellationToken)
    {
        BaseResponse<SubmitOrderResponse> result;
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            TblOrder order = new()
            {
                OrderId = Ulid.NewUlid().ToString(),
                CreatedAt = DateTime.Now,
                GrandTotal = request.GrandTotal,
                InvoiceNo = $"INV-{DateTime.Now:yyyyMMddHHmmss}",
                IsDeleted = false
            };

            await _orderUnitOfWork.OrderRepository.AddAsync(order, cancellationToken);
            await _orderUnitOfWork.SaveChangesAsync(cancellationToken);

            if (request.OrderDetail is not null && request.OrderDetail.Count > 0)
            {
                foreach (var item in request.OrderDetail)
                {
                    TblOrderDetail orderDetail = new()
                    {
                        Id = Ulid.NewUlid().ToString(),
                        CreatedAt = DateTime.Now,
                        InvoiceNo = order.InvoiceNo,
                        Price = item.Price,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        SubTotal = item.SubTotal,
                    };

                    await _orderDetailUnitOfWork.OrderDetailRepository.AddAsync(orderDetail, cancellationToken);
                    await _orderDetailUnitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            transactionScope.Complete();
            result = BaseResponse<SubmitOrderResponse>.Success();
        }
        catch (Exception ex)
        {
            result = BaseResponse<SubmitOrderResponse>.Fail(ex);
        }

    result:
        return result;
    }
}
