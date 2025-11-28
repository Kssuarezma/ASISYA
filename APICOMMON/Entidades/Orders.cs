using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICOMMON.Entidades
{
    public partial class Orders
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public int ShipVia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Freight { get; set; }

        [StringLength(100)]
        public string? ShipName { get; set; }

        [StringLength(200)]
        public string? ShipAddress { get; set; }

        [StringLength(100)]
        public string? ShipCity { get; set; }

        [StringLength(100)]
        public string? ShipRegion { get; set; }

        [StringLength(20)]
        public string? ShipPostalCode { get; set; }

        [StringLength(100)]
        public string? ShipCountry { get; set; }
    }
}
