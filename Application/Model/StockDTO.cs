using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInventory.Application.Model
{
    public class StockDTO
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AvailableQuantity { get; set; } = 0;
        public class CreateStockDTO : StockDTO
        {
            public DateTime CreatedDate { get; set; } = DateTime.Now;
            public string CreatedBy { get; set; } = "";
        }

        public class UpdateStockDTO : StockDTO
        {
            public DateTime? UpdatedDate { get; set; }
            public string? UpdatedBy { get; set; }
        }

        public class ViewStockDTO : StockDTO
        {
            public ItemDTO Item { get; set; }
        }
    }
}
