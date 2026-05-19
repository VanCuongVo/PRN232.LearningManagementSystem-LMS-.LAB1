using Microsoft.AspNetCore.Mvc;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Services;

namespace PRN232.LMS.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectContrller : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectContrller(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] QueryParameters query)
        {
            var result = await _subjectService.GetAllAsync(query);
            return Ok(result);
        }
    }
}