using System;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Services.Interface;

public interface IImageService
{
    Task<ResponseDto<ImageUploadResult>> imageUploadAsync (IFormFile photo);
    Task<ResponseDto<DeletionResult>> deleteImageAsync (string publicId);
}
