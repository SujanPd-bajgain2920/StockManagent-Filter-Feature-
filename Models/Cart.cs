using System;
using System.Collections.Generic;

namespace StockManagentSystem.Models;

public partial class Cart
{
    public short CartId { get; set; }

    public short? PurchaseProductId { get; set; }

    public short? PurchasecategoryId { get; set; }

    public int Quantity { get; set; }

    public bool Status { get; set; }

    public virtual Product? PurchaseProduct { get; set; }

    public virtual Category? Purchasecategory { get; set; }
}
