using Microsoft.AspNetCore.Http;

namespace DocApi.Models
{
    public class Upload
    {
        public IFormFile files { get; set; }
    }
}
