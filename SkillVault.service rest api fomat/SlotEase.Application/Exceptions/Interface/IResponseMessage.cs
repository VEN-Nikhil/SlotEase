using Microsoft.AspNetCore.Mvc;

namespace SlotEase.Application.Exceptions.Interface;

public interface IResponseMessage
{
    IActionResult Ok<T>(T message);
    IActionResult Error<T>(T message);
    IActionResult NotFound<T>(T message);
    IActionResult BadRequest<T>(T message);
}
