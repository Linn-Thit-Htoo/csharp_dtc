using csharp_dtc.API.Models;
using csharp_dtc.API.Utils;
using MediatR;

namespace csharp_dtc.API.Features.Order.SubmitOrder;

public class SubmitOrderRequest : IRequest<BaseResponse<SubmitOrderResponse>>
{
    public decimal GrandTotal { get; set; }
    public List<OrderDetailRequestModel> OrderDetail { get; set; }
}
