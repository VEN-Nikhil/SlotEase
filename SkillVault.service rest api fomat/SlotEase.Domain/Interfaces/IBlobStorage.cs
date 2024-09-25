using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using SlotEase.Domain.Constants;

namespace SlotEase.Domain.Interfaces;

public interface IBlobStorage
{
    Task<byte[]> GetBlobFIle(string path, string connectionString = null, string containerName = null);
    Task<string> UploadFileToStorage(IFormFile imageToUpload, string fileName, string imageFolder = ClientspaceConstants.OtherFolder);
    Task<string> ReplaceBlobImage(byte[] fileBytes, string filePath);
    Task<string> UploadBrokerSpaceBlobFileToTempFolder(string blobFileName, string tempFolderPath);
}
