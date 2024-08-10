using Back_end.Models;
using Back_end.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> getAllRequests()
        {
            var result = await _requestService.getAllRequests();

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> createRequest(CreateRequestDto request)
        {
            var newRequest = await _requestService.createRequest(request);

            return StatusCode(201);
        }
    }
}
