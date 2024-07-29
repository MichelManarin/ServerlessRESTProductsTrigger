using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ServerlessRESTProductsTrigger.Model
{
    [Table("product")]
    [Index("Sku", Name = "UQ__product__CA1ECF0D4D26C4DA", IsUnique = true)]
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int StoreId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [Column("SKU")]
        [StringLength(50)]
        public string Sku { get; set; } = null!;
        [StringLength(255)]
        public string? Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("Products")]
        public virtual Company Company { get; set; } = null!;
        [ForeignKey("StoreId")]
        [InverseProperty("Products")]
        public virtual Store Store { get; set; } = null!;
    }
}
