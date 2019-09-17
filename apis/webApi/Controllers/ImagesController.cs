using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace webApi.Controllers
{
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        public ImagesController(IHostingEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        [Route("api/Images/{category}/{pictureFileName}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // GET: /<controller>/
        public ActionResult GetImageAsync(string category, string pictureFileName)
        {
            if (string.IsNullOrEmpty(pictureFileName) || string.IsNullOrEmpty(category))
            {
                return BadRequest();
            }

            string rootPicFolder = "pics\\" + category.ToLower();
            string rootHost = _env.ContentRootPath;
            string picPath = Path.Combine(rootHost, rootPicFolder);

            if (!System.IO.Directory.Exists(picPath))
            {
                return NotFound();
            }

            var path = Path.Combine(picPath, pictureFileName);

            string imageFileExtension = Path.GetExtension(pictureFileName);
            string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

            if (System.IO.File.Exists(path))
            {
                var buffer = System.IO.File.ReadAllBytes(path);
                return File(buffer, mimetype);
            }
            else
            {
                return NotFound();
            }

        }

        private string GetImageMimeTypeFromImageFileExtension(string extension)
        {
            string mimetype;

            switch (extension)
            {
                case ".png":
                    mimetype = "image/png";
                    break;
                case ".gif":
                    mimetype = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimetype = "image/jpeg";
                    break;
                case ".bmp":
                    mimetype = "image/bmp";
                    break;
                case ".tiff":
                    mimetype = "image/tiff";
                    break;
                case ".wmf":
                    mimetype = "image/wmf";
                    break;
                case ".jp2":
                    mimetype = "image/jp2";
                    break;
                case ".svg":
                    mimetype = "image/svg+xml";
                    break;
                default:
                    mimetype = "application/octet-stream";
                    break;
            }

            return mimetype;
        }
    }
}