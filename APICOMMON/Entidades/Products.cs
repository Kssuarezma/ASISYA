using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICOMMON.Entidades
{
    public partial class Products
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = null!;

        [Required]
        public int SupplierID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [StringLength(50)]
        public string? QuantityPerUnit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public int? UnitsOnOrder { get; set; }

        public int? ReorderLevel { get; set; }

        [Required]
        public bool Discontinued { get; set; }
    }
}
