using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuctionService.Services
{
    public class ImagesService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<ImagesService> _logger;

        public ImagesService(IConfiguration configuration, ILogger<ImagesService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            Account account = new Account(
                _configuration["Cloudinary:CloudName"],
                _configuration["Cloudinary:ApiKey"],
                _configuration["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            try
            {
                var deleteParams = new DeletionParams(publicId);
                var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                if (deleteResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Successfully deleted image with publicId: {publicId}");
                }
                else
                {
                    _logger.LogError($"Failed to delete image with publicId: {publicId}. Error: {deleteResult.Error?.Message}");
                }

                return deleteResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting image with publicId: {publicId}. Exception: {ex.Message}");
                return new DeletionResult { Result = "Error", Error = new Error { Message = ex.Message } };
            }
        }

        public async Task<ImageUploadResult> ImageUploadAsync(IFormFile photo)
        {
            string[] allowedTypes = { "image/jpeg", "image/png", "image/jpg" };

            if (photo == null || photo.Length <= 0)
            {
                _logger.LogWarning("Image file is null or empty.");
                throw new ArgumentException("Image cannot be null or empty");
            }

            if (!allowedTypes.Contains(photo.ContentType))
            {
                _logger.LogWarning($"Invalid image type: {photo.ContentType}. Allowed types are .png, .jpg, and .jpeg");
                throw new ArgumentException("Acceptable types are .png, .jpg, and .jpeg");
            }

            try
            {
                using var fs = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.FileName, fs)
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Successfully uploaded image: {uploadResult.PublicId}");
                }
                else
                {
                    _logger.LogError($"Failed to upload image. Error: {uploadResult.Error?.Message}");
                }

                return uploadResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while uploading image: {ex.Message}");
                throw new Exception("Image upload failed: " + ex.Message);
            }
        }
    }
}
