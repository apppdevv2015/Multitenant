using Microsoft.AspNetCore.Mvc;
using MultiTenant.Data.Model;
using MultiTenant.Services.IService;

namespace MultiTenant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultitenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        public MultitenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        /// <summary>
        /// get all the tenants into the system
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTenantsAsync")]
        public async Task<ActionResult<List<Tenant>>> GetAllTenantsAsync()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();
            return Ok(tenants);
        }
        /// <summary>
        /// used to get the tenant by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenantById(int id)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(id);
            if (tenant == null)
                return NotFound();

            return Ok(tenant);
        }

        /// <summary>
        /// used to get the tenant by host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        [HttpGet("GetTenantByHostAsync/{host}")]
        public IActionResult GetTenantByHostAsync(string host)
        {
            var tenant = _tenantService.GetTenantByHostAsync(host);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }
        /// <summary>
        /// used to upload the favicon by TenantId
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("{tenantId}/upload/favicon")]
        public async Task<ActionResult> UploadFavicon(int tenantId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            try
            {
                await _tenantService.UploadFaviconAsync(tenantId, file);
                return Ok("Favicon uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to upload favicon: {ex.Message}");
            }
        }
        /// <summary>
        /// used to upload the home banner by TenantId
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("{tenantId}/upload/homebanner")]
        public async Task<ActionResult> UploadHomeBanner(int tenantId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            try
            {
                await _tenantService.UploadHomeBannerAsync(tenantId, file);
                return Ok("Home banner uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to upload home banner: {ex.Message}");
            }
        }
        /// <summary>
        /// used to get Favicon by TenantId
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet("{tenantId}/favicon")]
        public async Task<ActionResult> GetFavicon(int tenantId)
        {
            try
            {
                var faviconBytes = await _tenantService.GetFaviconAsync(tenantId);
                if (faviconBytes != null)
                {
                    return File(faviconBytes, "image/png");
                }
                else
                {
                    return NotFound("Favicon not found for the tenant.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to get favicon: {ex.Message}");
            }
        }
        /// <summary>
        /// used to get the home banner by TenantId
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet("{tenantId}/homebanner")]
        public async Task<ActionResult> GetHomeBanner(int tenantId)
        {
            try
            {
                var homeBannerBytes = await _tenantService.GetHomeBannerAsync(tenantId);
                if (homeBannerBytes != null)
                {
                    return File(homeBannerBytes, "image/png");
                }
                else
                {
                    return NotFound("Home banner not found for the tenant.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to get home banner: {ex.Message}");
            }
        }
    }
}
