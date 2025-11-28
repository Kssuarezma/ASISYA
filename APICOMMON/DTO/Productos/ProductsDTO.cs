using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICOMMON.DTO.Productos
{
    public class ProductsDTO
    {
        public int Cantidad { get; set; }

        public int MinSupplierId { get; set; } = 1;
        public int MaxSupplierId { get; set; } = 5;
        public int MinCategoryId { get; set; } = 1;
        public int MaxCategoryId { get; set; } = 5;

    }

    public class ProductosFiltradosDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class productosListaDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }

        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }
    }


    public class ProductoDetalleArchivoDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;

        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }

        public string CategoryName { get; set; } = null!;
        public byte[]? CategoryPicture { get; set; }
    }

    public class ProductosActEliDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = null!;
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}
