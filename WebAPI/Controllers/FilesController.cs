using Library.BusinessLogicLayer.Blobs;
using Library.Common.Dtos;
using Library.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace WebAPI.Controllers
{
    [Route("/api")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public FilesController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [Route("upload")]
        [HttpPost]
        public IActionResult Upload([FromForm] string Name)
        {
            var response = new ServiceResponse<BlobDto>();
            var file = HttpContext.Request.Form.Files.GetFile("file");
            if (file == null)
                response.SetFailed();
            else
            {
                Blob blob = new Blob();
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    blob.Content = ms.ToArray();
                }
                blob.Name = Name??file.Name;
                blob.ContentType = file.ContentType;
                blob.File_Path = Guid.NewGuid().ToString();
                response.SetSuccess(_blobService.Create(blob));
            }
            return Ok(response);
        }

        [HttpGet("files/{path}")]
        public IActionResult GetFileByPath([FromRoute] string path)
        {
            var blob = _blobService.FindPath(path);
            if (blob == null)
                return NotFound();
            return File(blob.Content, blob.ContentType);
        }
    }
}
