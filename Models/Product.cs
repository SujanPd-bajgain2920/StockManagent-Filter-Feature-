using System;
using System.Collections.Generic;

namespace StockManagentSystem.Models;

public partial class Product
{
    public short ProId { get; set; }

    public string ProName { get; set; } = null!;

    public decimal ProPrice { get; set; }

    public int Stock { get; set; }

    public string Description { get; set; } = null!;

    public string ProImage { get; set; } = null!;

    public string BrandName { get; set; } = null!;

    public short? CategoryId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

}
