using System.ComponentModel.DataAnnotations;

public class ProductViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(50)]
    public string Sku { get; set; } = null!;
    
    [Required]
    public string? Description { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CompanyId must be greater than zero.")]
    public int CompanyId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "StoreId must be greater than zero.")]
    public int StoreId { get; set; }
}