using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ServerlessRESTProductsTrigger.Model
{
    [Table("company")]
    public partial class Company
    {
        public Company()
        {
            Products = new HashSet<Product>();
            Stores = new HashSet<Store>();
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

        [InverseProperty("Company")]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Store> Stores { get; set; }
    }
}
