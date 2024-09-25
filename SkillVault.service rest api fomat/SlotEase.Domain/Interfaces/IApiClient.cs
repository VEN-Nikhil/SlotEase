using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlotEase.Domain.Interfaces;

public interface IApiClient
{
    Task<string> Delete(string url, string token, List<KeyValuePair<string, string>> extraHeaders);
    Task<dynamic> Get(string url, string token, List<KeyValuePair<string, string>> extraHeaders, bool returnString = true);
    Task<T> Get<T>(string url, string token, List<KeyValuePair<string, string>> extraHeaders);
    Task<string> GetContentAsString(string url, string token, List<KeyValuePair<string, string>> extraHeaders);
    Task<byte[]> Post<T>(string url, T data, string token, List<KeyValuePair<string, string>> extraHeaders);
    Task<byte[]> Put<T>(string url, T data, string token, List<KeyValuePair<string, string>> extraHeaders);
}