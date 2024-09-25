

using System.Net;
using SlotEase.Helpers;

namespace SlotEase.Application.Helpers;

/// <summary>
/// Helper class for creating standardized API responses.
/// </summary>
public static class ApiResponseHelper
{
    /// <summary>
    /// Creates a standardized Not Found response.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="message">The status message for the response.</param>
    /// <returns>An ApiResponse with a Not Found status and the specified message.</returns>
    public static ApiResponse<T> NotFoundResponse<T>(string message)
    {
        return new ApiResponse<T>
        {
            Data = default,
            Status = (int)HttpStatusCode.NotFound,
            StatusMessage = message
        };
    }

    /// <summary>
    /// Creates a standardized OK response.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="data">The data to include in the response.</param>
    /// <param name="message">The status message for the response.</param>
    /// <returns>An ApiResponse with an OK status, the specified data, and message.</returns>
    public static ApiResponse<T> OkResponse<T>(T data, string message)
    {
        return new ApiResponse<T>
        {
            Data = data,
            Status = (int)HttpStatusCode.OK,
            StatusMessage = message
        };
    }

    /// <summary>
    /// Creates a standardized Internal Server Error response.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="message">The status message for the response.</param>
    /// <returns>An ApiResponse with an Internal Server Error status and the specified message.</returns>
    public static ApiResponse<T> InternalServerErrorResponse<T>(string message)
    {
        return new ApiResponse<T>
        {
            Data = default,
            Status = (int)HttpStatusCode.InternalServerError,
            StatusMessage = message
        };
    }

    /// <summary>
    /// Creates a standardized Bad Request response.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="message">The status message for the response.</param>
    /// <returns>An ApiResponse with a Bad Request status and the specified message.</returns>
    public static ApiResponse<T> BadRequestResponse<T>(string message)
    {
        return new ApiResponse<T>
        {
            Data = default,
            Status = (int)HttpStatusCode.BadRequest,
            StatusMessage = message
        };
    }
}
