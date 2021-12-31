using System.Collections.Generic;
using System.Threading.Tasks;
using Examination.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Examination.API.Controllers.V1;


[ApiController]
[ApiVersion( "1.0" )]
[Route( "api/v{version:apiVersion}/[controller]" )]
public class ExamControllers :  ControllerBase
{
        private readonly IMediator _mediator;

        public ExamControllers(IMediator mediator)
        { 
                _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetExamList()
        {
            var exams= await _mediator.Send(new GetHomeExamListQuery() { });
            return Ok(exams);
        }
}