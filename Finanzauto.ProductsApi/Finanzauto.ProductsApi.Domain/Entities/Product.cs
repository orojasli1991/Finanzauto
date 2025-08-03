using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzauto.ProductsApi.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "el precio debe ser mayor a cero")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "el stock debe ser mayor a cero")]
        public int Stock { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
    }
}
