using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        [Required]
        [Range(0.001, double.MaxValue)]
        public decimal Price { get; set; }

        public class CreateItemDTO : ItemDTO
        {
            public DateTime CreatedDate { get; set; } = DateTime.Now;
            public string CreatedBy { get; set; } = "";
        }

        public class UpdateItemDTO: ItemDTO
        {
            public DateTime? UpdatedDate { get; set; }
            public string? UpdatedBy { get; set; }

        }
    }
}
