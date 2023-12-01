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

        [HttpPost("CreateEarTagAsync")]
        public async Task<ActionResult<EarTag>> CreateEarTagAsync(EarTagInsertModel earTag)
        {
            if (earTag.CountryCode.Length != 2)
            {
                return BadRequest("Country code should be alpha-2.");
            }
            if (earTag.CentraleHusdyrbrugsRegisterNumber <= 0)
            {
                return BadRequest("CHR cannot be empty or 0.");
            }
            if (earTag.HerdNumber <= 0)
            {
                return BadRequest("Herd number cannot be empty or 0.");
            }

            EarTag tempEarTag = await _earTagService.AddEarTagAsync(earTag);

            return CreatedAtAction(nameof(GetEarTagByID), new { id = tempEarTag.ID }, tempEarTag);
        }

        [HttpGet("GetEarTagByID")]
        public async Task<ActionResult<EarTag>> GetEarTagByID(int id)
        {
            try
            {
                EarTag earTag = await _earTagService.GetEarTagByIDAsync(id);

                if (earTag.CentraleHusdyrbrugsRegisterNumber == 0)
                {
                    return NotFound();
                }

                return Ok(earTag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving ear tag.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
