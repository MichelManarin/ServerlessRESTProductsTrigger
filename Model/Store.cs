using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ServerlessRESTProductsTrigger.Model
{
    [Table("store")]
    [Index("CompanyId", Name = "IX_store_CompanyId")]
    public partial class Store
    {
        public Store()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Column("Address_Street")]
        public string AddressStreet { get; set; } = null!;
        [Column("Address_City")]
        public string AddressCity { get; set; } = null!;
        [Column("Address_State")]
        public string AddressState { get; set; } = null!;
        [Column("Address_Country")]
        public string AddressCountry { get; set; } = null!;
        [Column("Address_ZipCode")]
        public string AddressZipCode { get; set; } = null!;
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("Stores")]
        public virtual Company Company { get; set; } = null!;
        [InverseProperty("Store")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
