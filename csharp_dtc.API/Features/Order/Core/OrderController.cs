using csharp_dtc.API.Features.Core;
using csharp_dtc.API.Features.Order.SubmitOrder;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csharp_dtc.API.Features.Order.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly ISender _sender;

        public OrderController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("SubmitOrder")]
        public async Task<IActionResult> SubmitOrder(SubmitOrderRequest request, CancellationToken cs)
        {
            var result = await _sender.Send(request, cs);
            return Content(result);
        }
    }
}
