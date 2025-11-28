using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICOMMON.Entidades
{
    public partial class Categories
    {
        [Key]
        [Column("CategoryID")]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("CategoryName")]
        public string CategoryName { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column("Description")]
        public string? Description { get; set; }

        [Column("Picture")]
        public byte[]? Picture { get; set; }
    }
}
