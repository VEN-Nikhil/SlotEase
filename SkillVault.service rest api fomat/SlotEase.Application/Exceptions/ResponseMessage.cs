using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SlotEase.Application.Exceptions.Interface;

namespace SlotEase.Application.Exceptions;

public class ResponseMessage : IResponseMessage
{
    public IActionResult Ok<T>(T message)
    {
        return new ContentResult()
        {
            Content = JsonConvert.SerializeObject(message),
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    public IActionResult Error<T>(T message)
    {
        return new ContentResult()
        {
            Content = JsonConvert.SerializeObject(message),
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }

    public IActionResult NotFound<T>(T message)
    {
        return new ContentResult()
        {
            Content = JsonConvert.SerializeObject(message),
            StatusCode = (int)HttpStatusCode.NotFound
        };
    }

    public IActionResult BadRequest<T>(T message)
    {
        return new ContentResult()
        {
            Content = JsonConvert.SerializeObject(message),
            StatusCode = (int)HttpStatusCode.BadRequest
        };
    }
}
