using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BJK.Finance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefaultSettingsController : Controller
    {
        private readonly AppSettings _appSettings;

        public DefaultSettingsController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value; // Access the settings
        }

        [HttpGet]
        public IActionResult GetSettings()
        {
            return Ok(_appSettings);
        }
    }
}