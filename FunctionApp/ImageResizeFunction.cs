using FunctionApp.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace FunctionApp
{
    [StorageAccount("BlobConnection")]
    public class ImageResizeFunction
    {
        private readonly IImageResizer _imageResizer;

        public ImageResizeFunction(IImageResizer imageResizer)
        {
            _imageResizer = imageResizer;
        }

        [FunctionName("ResizeCoverImage")]
        public void Run(
            [BlobTrigger("imagecontainer/{name}")] Stream inputBlob, 
            [Blob("reduced-size/{name}", FileAccess.Write)] Stream outputBlob,
            string name, 
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {inputBlob.Length} Bytes");

            try
            {
                _imageResizer.Resize(inputBlob, outputBlob);
                log.LogError("Resized successfully, image saved to blob storage: ");
            }
            catch (Exception e)
            {

                log.LogError("Resize Failed: ", e);
            }
        }
    }
}
