namespace SdnMonitorApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using Models;
    using SdnMonitorCore;
    using SdnMonitorData;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("[controller]")]
    public class SdnListController : ControllerBase
    {
        private readonly ILogger<SdnListController> _logger;
        private readonly SdnListContext _context;

        public SdnListController(ILogger<SdnListController> logger, SdnListContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public SdnChangeSummary Get()
        {
            var sm = new SdnMonitor(_context);

            var sdnChangeSummary = sm.CompareSdnListAsync();
            sdnChangeSummary.Wait();

            return sdnChangeSummary.Result;
        }
    }
}
