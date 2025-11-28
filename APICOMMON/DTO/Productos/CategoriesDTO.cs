using Microsoft.AspNetCore.Http;

namespace APICOMMON.DTO.Productos
{
    public class CategoriesDTO
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
        //public IFormFile? Picture { get; set; }

    }
}
