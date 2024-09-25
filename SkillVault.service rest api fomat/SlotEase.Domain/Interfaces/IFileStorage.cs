using System.IO;
using System.Threading.Tasks;

namespace SlotEase.Domain.Interfaces;

public interface IFileStorage
{
    Task<byte[]> GetFileToBlobStorage(string path, string connectionString = null, string shareName = null);
    Task<string> GetTemplate(string filepath);
    Task UploadFileToStorageFromStream(MemoryStream ms, string folder, string fileName, string connectionString = null, string shareName = null);
    Task<string> GetSASUriAsync(string filepath);

}