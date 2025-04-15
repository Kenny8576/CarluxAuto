using System;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Services;

public interface IImageService
{
    Task<ImageUploadResult> ImageUploadAsync (IFormFile photo);
    Task<DeletionResult> DeleteImageAsync (string publicId);
}

