using System;
using System.Collections.Generic;

namespace StockManagentSystem.Models;

public partial class Category
{
    public short CatId { get; set; }

    public string CatName { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
