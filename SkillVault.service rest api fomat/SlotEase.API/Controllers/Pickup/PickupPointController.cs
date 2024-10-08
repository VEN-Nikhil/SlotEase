using SlotEase.Application.Commands.PickupPoint;
using SlotEase.Application.DTO.PickpPoint;
using SlotEase.Application.DTO.PickupPoint;
using SlotEase.Application.Interfaces;
using System.Collections.Generic;

namespace SlotEase.API.Controllers.Pickup
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    [Authorize]
    public class PickupPointController(IPickupPoint PickupPointQueries, IMediator mediator) : ControllerBase
    {
        private readonly IPickupPoint _pickpPointQueries = PickupPointQueries ?? throw new ArgumentNullException(nameof(PickupPointQueries));
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));



        [AllowAnonymous]
        [HttpPost]
        [Route("PickupPoints")]
        public async Task<IActionResult> GetPickupPoints(PickpPointRequestDto pickpPointRequest)
        {
            try
            {
                List<PickupPointDto> pickupPoints = await _pickpPointQueries.GetAllPickupPoint(pickpPointRequest);
                return Ok(pickupPoints);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the pickup points: {ex.Message}");
            }
        }




        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSinglePickupPoint(int id)
        {
            try
            {
                PickupPointDto pickupPoint = await _pickpPointQueries.GetPickupPointByIdAsync(id);

                return Ok(pickupPoint);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreatePickupPoint(PickupPointDto pickupPointDto)
        {
            var obj_Res = new CreatedPickupPointCommand(pickupPointDto);
            var result = await _mediator.Send(obj_Res);
            return Ok(result);
        }

    }
}
