namespace csharp_dtc.API.OrderDetailDbContextModels;

public partial class TblOrderDetail
{
    public string Id { get; set; } = null!;

    public string InvoiceNo { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal SubTotal { get; set; }

    public DateTime CreatedAt { get; set; }
}
