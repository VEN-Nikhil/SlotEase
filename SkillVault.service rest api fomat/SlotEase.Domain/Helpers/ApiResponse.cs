using SlotEase.Domain.Helpers;

namespace SlotEase.Helpers;

public class ApiResponse<T>
{
    public ApiResponse()
    {
        Status = 0;
    }
    public int Status { get; set; }
    public string StatusMessage { get; set; }
    public T Data { get; set; }
    public AppMessage Message { get; set; }
}

public class ApiResponse
{
    public ApiResponse()
    {
        Status = 0;

    }
    public int Status { get; set; }
    public string StatusMessage { get; set; }
    public AppMessage Message { get; set; }
}