using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace swagger.webapi.Controllers
{
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        [HttpPost]
        public IActionResult FileUpload2(IFormFile file)
        {


            //open File
            //var stream = file.OpenReadStream();
            //var name = Path.GetFileName(file.FileName);

            return Ok(file.FileName);

        }
    }
}
