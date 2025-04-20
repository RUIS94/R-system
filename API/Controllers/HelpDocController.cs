using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpDocController : ControllerBase
    {
        private readonly IHelpDocService _service;

        public HelpDocController(IHelpDocService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var docs = await _service.GetAllHelpDocsAsync();
            return Ok(docs);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] HelpDoc helpDoc)
        {
            try
            {
                var success = await _service.AddHelpDocAsync(helpDoc);
                return success ? Ok("Help document added successfully") : BadRequest("Failed to add help document");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}