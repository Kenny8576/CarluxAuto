using ImageService.Services;
using ImageService.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService )
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<ActionResult> uploadImage(IFormFile photo)
        {
            var result = await _imageService.imageUploadAsync(photo);
            
            return Ok(result);
        }

        public async Task<ActionResult> deleteImage(string id)
        {
            var result = await _imageService.deleteImageAsync(id);
            return Ok(result);
        }

    }
}
