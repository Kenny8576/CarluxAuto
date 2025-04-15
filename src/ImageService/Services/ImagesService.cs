using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ImageService.Services.Interface;
using MassTransit;

namespace ImageService.Services;

public class ImagesService : IImageService
{
     private readonly IConfiguration _config;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly Cloudinary cloudinary;
    public ImagesService(IConfiguration config, IPublishEndpoint publishEndpoint)
    {
         _config = config;
        _publishEndpoint = publishEndpoint;
        Account account = new Account (
            _config["Cloudinary:CloudName"],
            _config["Cloudinary:ApiKey"],
            _config["Cloudinary:ApiSecret"]
        );

        cloudinary = new Cloudinary(account);
    }
    public async Task<ResponseDto<DeletionResult>> deleteImageAsync(string publicId)
    {
          var errors = new List<Error>();

        try {
            var deleteParams = new DeletionParams(publicId);
            var deleteResult = await cloudinary.DestroyAsync(deleteParams);
            return ResponseDto<DeletionResult>.Success(deleteResult, "Successfully deleted Image");

        }
          catch (Exception ex)
         {
           errors.Add(new Error("500", "Unexpected error occured: " + ex.Message));
             return ResponseDto<DeletionResult>.Failure(errors, 500);
         }
    }

    public async Task<ResponseDto<ImageUploadResult>> imageUploadAsync(IFormFile photo)
    {
            string[] allowedTypes = { "image/jpeg", "image/png", "image/jpg" };
    if (photo == null || photo.Length <= 0)
    {
        throw new ArgumentException("Image cannot be null or empty");
    }
    
    if (!allowedTypes.Contains(photo.ContentType))
    {
        throw new ArgumentException("Acceptable types are .png, .jpg and .jpeg") ;
    }
        ImageUploadResult imageUploadResult;
    try
    {
        using var fs = photo.OpenReadStream();
        imageUploadResult = await cloudinary.UploadAsync(new ImageUploadParams()
        {
            File = new FileDescription(photo.FileName, fs)
        });
    }
       catch (Exception ex)
    {
        throw new Exception("Image upload failed: " + ex.Message);
    }
 
        return ResponseDto<ImageUploadResult>.Success(imageUploadResult, "Successfully Uploaded Image", 200);
    }
}


   
   

  
