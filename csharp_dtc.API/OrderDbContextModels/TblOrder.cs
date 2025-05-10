using System;
using System.Collections.Generic;

namespace csharp_dtc.API.OrderDbContextModels;

public partial class TblOrder
{
    public string OrderId { get; set; } = null!;

    public string InvoiceNo { get; set; } = null!;

    public decimal GrandTotal { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }
}
