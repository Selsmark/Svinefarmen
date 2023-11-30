using Microsoft.AspNetCore.Mvc;
using Web_API.Services;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarTagController : Controller
    {
        private readonly IEarTagService _earTagService;

        public EarTagController(IEarTagService earTagService)
        {
            _earTagService = earTagService;
        }

        [HttpPost]
        public IActionResult CreateEarTag(string countryCode, int centraleHusdyrbrugsRegisterNumber, int herdNumber)
        {
            int earTagID = _earTagService.InsertEarTag(countryCode, centraleHusdyrbrugsRegisterNumber, herdNumber);

            if (earTagID > 0)
            {
                return CreatedAtRoute("GetEarTag", new { id = earTagID }, null);
            }
            else
            {
                return BadRequest("Failed to create ear tag.");
            }
        }
    }
}
