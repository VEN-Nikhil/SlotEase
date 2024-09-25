using Microsoft.AspNetCore.Mvc;

namespace SlotEase.API.Controllers.General;


/// <summary>
/// 
/// </summary>
public class HomeController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return new RedirectResult("~/swagger");
    }
}