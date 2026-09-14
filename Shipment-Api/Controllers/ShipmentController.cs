using Microsoft.AspNetCore.Mvc;
using Shipment_Api.Models;
using Shipment_Api.Services;

namespace Shipment_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController(ILogger<ShipmentController> logger, IShipmentService shipmentService) : ControllerBase
    {
        /// <summary>
        /// Return a paged list of shipments.
        /// </summary>
        /// <param name="requestDto">Query input parameters</param>
        /// <param name="cancellationToken">Token that can be used to cancel a request</param>
        /// <response code="200">Shipments successfully retrieved</response>
        /// <response code="400">Bad request due to input parameters</response>
        /// <returns>A paged list of shipments</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<Shipment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResult<Shipment>>> GetShipments(
            [FromQuery] ShipmentRequestDto requestDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                logger.LogDebug("Retrieving shipments. Page number: {PageNumber}, page size: {PageSize}, filter: {Filter}", requestDto.PageNumber, requestDto.PageSize, requestDto.Filter);

                // simulate a delay for an asynchronous call
                await Task.Delay(TimeSpan.FromMilliseconds(200), cancellationToken);

                // NOTE: This is not awaited as an in-memory list is used for the shipments
                var shipments = shipmentService.GetShipments(requestDto.PageNumber, requestDto.PageSize, requestDto.Filter);

                return Ok(shipments);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred retrieving shipments. Page number: {PageNumber}, page size: {PageSize}, filter: {Filter}", requestDto.PageNumber, requestDto.PageSize, requestDto.Filter);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred retrieving shipments");
            }
        }

        [HttpGet("test-unhandled-error")]
        public IActionResult TestUnhandledError()
        {
            throw new TimeoutException("deliberate error to test global exception handling");
        }
    }
}
