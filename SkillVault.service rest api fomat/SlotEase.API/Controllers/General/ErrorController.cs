using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SlotEase.API.Controllers.General;

/// <summary>
/// 
/// </summary>
[ApiController]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("/error")]
    public IActionResult Error()
    {
        return HandleError();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost("/error")]
    public IActionResult PostError()
    {
        return HandleError();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPut("/error")]
    public IActionResult PutError()
    {
        return HandleError();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpDelete("/error")]
    public IActionResult DeleteError()
    {
        return HandleError();
    }

    private IActionResult HandleError()
    {
        IExceptionHandlerFeature context = HttpContext?.Features?.Get<IExceptionHandlerFeature>();
        _logger.LogError(context?.Error, context?.Error?.Message);
        return Problem();
    }

}