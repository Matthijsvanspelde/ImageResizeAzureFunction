using System.IO;

namespace FunctionApp.Services
{
    public interface IImageResizer
    {
        void Resize(Stream input, Stream output);
    }
}
