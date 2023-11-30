using Microsoft.AspNetCore.Mvc;
using Web_API.Data;
using Web_API.Services;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StableController : ControllerBase
    {
        private static readonly string[] CountryCodesAlpha2 = new[]
        {
            "DK", "SE", "NO", "DE"
        };

        private readonly ILogger<StableController> _logger;
        private readonly IStableService _stableStockService;

        public StableController(ILogger<StableController> logger, IStableService stableStockService)
        {
            _logger = logger;
            _stableStockService = stableStockService;
        }

        [HttpGet("GetAllStables")]
        public async Task<ActionResult<List<Stable>>> GetAllStables()
        {
            try
            {
                List<Stable> stables = await _stableStockService.GetAllStablesAsync();

                if (!stables.Any())
                {
                    return NoContent();
                }

                return Ok(stables);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving stables.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("GetEarTagsByStableID")]
        public async Task<ActionResult<List<EarTag>>> GetEarTagsByStableID(int stableID)
        {
            try
            {
                List<EarTag> earTags = await _stableStockService.GetEarTagsByStableIDAsync(stableID);

                if (!earTags.Any())
                {
                    return NoContent();
                }

                return Ok(earTags);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving stables.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("GetHerdsByStableID")]
        public async Task<ActionResult<List<EarTag>>> GetHerdsByStableID(int stableID)
        {
            try
            {
                List<int> herds = await _stableStockService.GetHerdNumbersByStableIDAsync(stableID);

                if (!herds.Any())
                {
                    return NoContent();
                }

                return Ok(herds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving stables.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("GetAllStableInfos")]
        public async Task<ActionResult<List<StableInfo>>> GetAllStableInfosAsync()
        {
            try
            {
                List<StableInfo> stableInfos = await _stableStockService.GetAllStableInfosAsync();

                if (!stableInfos.Any())
                {
                    return NoContent();
                }

                return Ok(stableInfos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving stables.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /*
        [HttpGet("GetDummyEarTags")]
        public IEnumerable<EarTag> GetDummyEarTags([FromQuery] int count)
        {
            return Enumerable.Range(1, count).Select(index => new EarTag
            {
                CountryCodeAlpha2 = CountryCodesAlpha2[Random.Shared.Next(CountryCodesAlpha2.Length)],
                Chr = Random.Shared.Next(0, 999999),
                HerdNumber = Random.Shared.Next(0, 99999)
            })
            .ToArray();
        }

        [HttpGet("GetEarTags")]
        public async Task<ActionResult<List<EarTag>>> GetEarTags([FromQuery] string? pigstyName = null)
        {
            if (string.IsNullOrWhiteSpace(pigstyName))
            {
                return await _earTagService.GetAllEarTagsAsync();
                //return BadRequest("The 'pigstyName' parameter is required.");
            }

            return null!;
        }

        */

        /*
        [HttpGet("GetAllEarTags")]
        public async Task<ActionResult<EarTag>> GetAllEarTags()
        {

        }
        */
    }
}
