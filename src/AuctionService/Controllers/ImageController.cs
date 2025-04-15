using AuctionService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        
        [HttpPost]
        public async Task<ActionResult> uploadImage(IFormFile photo)
        {

            var result = await _imageService.ImageUploadAsync(photo);
            
            return Ok(result);
        }

        
        [HttpDelete]
        public async Task<ActionResult> deleteImage(string id)
        {
            var result = await _imageService.DeleteImageAsync(id);
            return Ok(result);
        }
    }
}
