using Microsoft.AspNetCore.Components.Forms;

namespace Flipzon_Server.Service.IService
{
    public interface IFileUpload
    {

        Task<string> Upload(IBrowserFile file);

        public bool DeleteFile(string filePath);
    }
}
