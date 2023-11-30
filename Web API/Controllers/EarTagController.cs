using Microsoft.AspNetCore.Mvc;
using Web_API.Data;
using Web_API.Services;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarTagController : Controller
    {
        private readonly ILogger<EarTagController> _logger;
        private readonly IEarTagService _earTagService;

        public EarTagController(ILogger<EarTagController> logger, IEarTagService earTagService)
        {
            _logger = logger;
            _earTagService = earTagService;
        }

        [HttpPost]
        public async Task<ActionResult<EarTag>> CreateEarTagAsync(EarTag earTag)
        {
            if (earTag.CountryCode.Length != 2)
            {
                return BadRequest("Country code should be alpha-2.");
            }
            if (earTag.CentraleHusdyrbrugsRegisterNumber == null)
            {
                return BadRequest("CHR cannot be null.");
            }
            if (earTag.HerdNumber == null)
            {
                return BadRequest("Herd number cannot be null.");
            }

            int earTagID = await _earTagService.AddEarTagAsync(earTag);

            return CreatedAtAction(nameof(GetEarTagByID), new { id = earTagID }, earTag);
        }

        [HttpGet("GetEarTagByID")]
        public async Task<ActionResult<EarTag>> GetEarTagByID(int ID)
        {
            try
            {
                EarTag earTag = await _earTagService.GetEarTagByID(ID);

                if (earTag == null)
                {
                    return NotFound();
                }

                return Ok(earTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving stables.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
