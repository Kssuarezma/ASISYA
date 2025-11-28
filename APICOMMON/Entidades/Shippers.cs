using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICOMMON.Entidades
{
    public partial class Shippers
    {
        [Key]
        public int ShipperID { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = null!;

        [StringLength(50)]
        public string? Phone { get; set; }
    }
}
