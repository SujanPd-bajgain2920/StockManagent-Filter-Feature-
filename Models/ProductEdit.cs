using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StockManagentSystem.Models
{
    public class ProductEdit
    {
        public short ProId { get; set; }

        public string ProName { get; set; } = null!;

        public decimal ProPrice { get; set; }

        public int Stock { get; set; }

        public string Description { get; set; } = null!;

        public string ProImage { get; set; } = null!;

        public string BrandName { get; set; } = null!;

        public short? CategoryId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProductFile { get; set; } = null!;

        public short FetchCategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public ProductEdit Product { get; set; }
    }
}
